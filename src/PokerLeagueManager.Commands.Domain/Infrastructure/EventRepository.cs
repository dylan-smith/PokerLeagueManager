using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.Common.Utilities.Exceptions;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        private IDatabaseLayer _databaseLayer;
        private IGuidService _guidService;
        private IDateTimeService _dateTimeService;
        private IEventSubscriberFactory _eventSubscriberFactory;

        public EventRepository(
            IDatabaseLayer databaseLayer,
            IGuidService guidService,
            IDateTimeService dateTimeService,
            IEventSubscriberFactory eventSubscriberFactory)
        {
            _databaseLayer = databaseLayer;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
            _eventSubscriberFactory = eventSubscriberFactory;
        }

        public void PublishEvents(IAggregateRoot aggRoot, ICommand c, Guid originalVersion)
        {
            if (aggRoot.PendingEvents == null || aggRoot.PendingEvents.Count == 0)
            {
                return;
            }

            if (aggRoot.AggregateVersion != originalVersion)
            {
                throw new OptimisticConcurrencyException(string.Format("Aggregate ID: {0} - Original Version: {1}", aggRoot.AggregateId, originalVersion));
            }

            PublishEvents(aggRoot, c);
        }

        public void PublishEvents(IAggregateRoot aggRoot, ICommand c)
        {
            if (aggRoot.PendingEvents == null || aggRoot.PendingEvents.Count == 0)
            {
                return;
            }

            ExecuteWithLockedAggregate(aggRoot.AggregateId, () =>
                {
                    ValidateAggregateOptimisticConcurrency(aggRoot);
                    PersistEventsInTransaction(aggRoot, c);
                });

            PublishEventsToSubscribers(aggRoot.PendingEvents);
        }

        private void ExecuteWithLockedAggregate(Guid aggregateId, Action work)
        {
            DeleteExpiredLocks();

            AcquireAggregateLock(aggregateId);
            work();
            ReleaseAggregateLock(aggregateId);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        private void AcquireAggregateLock(Guid aggregateId)
        {
            try
            {
                _databaseLayer.ExecuteNonQuery("INSERT INTO AggregateLocks(AggregateId, LockExpiry) VALUES(@AggregateId, @LockExpiry)", 
                    "@AggregateId", aggregateId.ToString(),
                    "@LockExpiry", _dateTimeService.UtcNow().AddMinutes(1));
            }
            catch
            {
                throw new OptimisticConcurrencyException(string.Format("Aggregate ID {0} is currently being written to by another process", aggregateId));
            }
        }

        private void DeleteExpiredLocks()
        {
            _databaseLayer.ExecuteNonQuery("DELETE FROM AggregateLocks WHERE LockExpiry < @CurrentDateTime", "@CurrentDateTime", _dateTimeService.UtcNow());
        }

        private void ValidateAggregateOptimisticConcurrency(IAggregateRoot aggRoot)
        {
            if (aggRoot.AggregateVersion == Guid.Empty)
            {
                return;
            }

            var currentVersion = GetAggregateVersion(aggRoot.AggregateId);

            if (currentVersion != aggRoot.AggregateVersion)
            {
                throw new OptimisticConcurrencyException(string.Format("Aggregate ID: {0} - Original Version: {1}", aggRoot.AggregateId, aggRoot.AggregateVersion));
            }
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        private Guid GetAggregateVersion(Guid aggregateId)
        {
            return (Guid)_databaseLayer.ExecuteScalar(
                "SELECT TOP 1 EventId FROM Events WHERE AggregateId = @AggregateId ORDER BY EventTimestamp DESC",
                "@AggregateId", aggregateId.ToString());
        }

        private void PersistEventsInTransaction(IAggregateRoot aggRoot, ICommand c)
        {
            _databaseLayer.ExecuteInTransaction(() =>
            {
                foreach (var e in aggRoot.PendingEvents)
                {
                    PersistEventToDatabase(e, c, aggRoot.AggregateId);
                }
            });
        }

        private void ReleaseAggregateLock(Guid aggregateId)
        {
            _databaseLayer.ExecuteNonQuery("DELETE FROM AggregateLocks WHERE AggregateId = @AggregateId", "@AggregateId", aggregateId);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        public bool DoesAggregateExist(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
            {
                return false;
            }

            int eventCount = (int)_databaseLayer.ExecuteScalar(
                "SELECT COUNT(*) FROM Events WHERE AggregateId = @AggregateId",
                "@AggregateId", aggregateId.ToString());

            if (eventCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        public T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot
        {
            if (aggregateId == Guid.Empty)
            {
                throw new ArgumentException(string.Format("Invalid Aggregate ID ({0})", aggregateId.ToString()));
            }

            T result = (T)System.Activator.CreateInstance(typeof(T), true);

            var allEvents = _databaseLayer.GetDataTable(
                "SELECT EventData, EventType FROM Events WHERE AggregateId = @AggregateId ORDER BY EventTimestamp",
                "@AggregateId", aggregateId.ToString());

            if (allEvents.Rows.Count == 0)
            {
                throw new ArgumentException(string.Format("No Aggregate with the specified ID was found ({0})", aggregateId.ToString()));
            }

            foreach (DataRow row in allEvents.Rows)
            {
                var e = CreateEventFromDataRow(row);
                result.ApplyEvent(e);
                result.AggregateVersion = e.EventId;
            }

            return (T)result;
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        private void PersistEventToDatabase(IEvent e, ICommand c, Guid aggregateId)
        {
            e.EventId = _guidService.NewGuid();
            e.Timestamp = _dateTimeService.Now();
            e.CommandId = c.CommandId;
            e.AggregateId = aggregateId;

            var eventXml = SerializeEvent(e);
            var sql = "INSERT INTO Events(EventId, EventDateTime, CommandId, AggregateId, EventType, EventData, Published)";
            sql += " VALUES(@EventId, @EventDateTime, @CommandId, @AggregateId, @EventType, @EventData, @Published)";

            _databaseLayer.ExecuteNonQuery(
                sql,
                "@EventId", e.EventId,
                "@EventDateTime", e.Timestamp.ToString("dd-MMM-yyyy HH:mm:ss.ff"),
                "@CommandId", e.CommandId,
                "@AggregateId", e.AggregateId,
                "@EventType", e.GetType().AssemblyQualifiedName,
                "@EventData", eventXml,
                "@Published", false);
        }

        private void PublishEventsToSubscribers(IEnumerable<IEvent> events)
        {
            var subscribers = GetSubscribers();

            foreach (var e in events)
            {
                foreach (var s in subscribers)
                {
                    s.Publish(e);
                }

                MarkEventAsPublished(e);
            }
        }

        private void MarkEventAsPublished(IEvent e)
        {
            _databaseLayer.ExecuteNonQuery("UPDATE Events SET Published = 1 WHERE EventId = @EventId", "@EventId", e.EventId);
        }

        private IEnumerable<EventSubscriber> GetSubscribers()
        {
            var subscriberTable = _databaseLayer.GetDataTable("SELECT * FROM Subscribers");

            foreach (DataRow row in subscriberTable.Rows)
            {
                yield return _eventSubscriberFactory.Create(row);
            }
        }

        private string SerializeEvent<T>(T e)
        {
            var serializer = new DataContractSerializer(e.GetType());

            using (var memStream = new MemoryStream())
            {
                serializer.WriteObject(memStream, e);

                memStream.Position = 0;
                var reader = new StreamReader(memStream);

                return reader.ReadToEnd();
            }
        }

        private IEvent CreateEventFromDataRow(DataRow row)
        {
            var eventType = Type.GetType((string)row["EventType"], true);
            var ser = new DataContractSerializer(eventType);

            using (var xr = new XmlReaderFacade((string)row["EventData"]))
            {
                return (IEvent)ser.ReadObject(xr.XmlReader);
            }
        }
    }
}
