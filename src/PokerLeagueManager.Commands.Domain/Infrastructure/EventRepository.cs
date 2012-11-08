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

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        public void PublishEvent(IEvent e, ICommand c, Guid aggregateId)
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

            var subscribers = GetSubscribers();

            foreach (var s in subscribers)
            {
                s.Publish(e);
            }

            MarkEventAsPublished(e);
        }

        public void PublishEvents(IAggregateRoot aggRoot, ICommand c)
        {
            foreach (var e in aggRoot.PendingEvents)
            {
                PublishEvent(e, c, aggRoot.AggregateId);
            }
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

            T result = System.Activator.CreateInstance<T>();

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
            }

            return (T)result;
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

            using (var memStream = new System.IO.MemoryStream())
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
