using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        IDatabaseLayer _databaseLayer;
        IGuidService _guidService;
        IDateTimeService _dateTimeService;

        public EventRepository(IDatabaseLayer databaseLayer, IGuidService guidService, IDateTimeService dateTimeService)
        {
            _databaseLayer = databaseLayer;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
        }

        public void PublishEvent(IEvent e, ICommand c, Guid aggregateId)
        {
            e.EventId = _guidService.NewGuid();
            e.Timestamp = _dateTimeService.Now();
            e.User = c.User;
            e.CommandId = c.CommandId;
            e.AggregateId = aggregateId;

            var eventXml = SerializeEvent(e);
            var sql = "INSERT INTO Events(EventId, EventDateTime, UserName, CommandId, AggregateId, EventType, EventData, Published)";
            sql += " VALUES(@EventId, @EventDateTime, @UserName, @CommandId, @AggregateId, @EventType, @EventData, @Published)";

            _databaseLayer.ExecuteNonQuery(sql,
                "@EventId", e.EventId,
                "@EventDateTime", e.Timestamp.ToString("dd-MMM-yyyy HH:mm:ss.ff"),
                "@UserName", e.User,
                "@CommandId", e.CommandId,
                "@AggregateId", e.AggregateId,
                "@EventType", e.GetType().AssemblyQualifiedName,
                "@EventData", eventXml,
                "@Published", false);

            // TODO: Need to publish this to all subscribers
        }

        public void PublishEvents(IAggregateRoot aggRoot, ICommand c)
        {
            // TODO: make sure this is all in a transaction
            foreach (var e in aggRoot.PendingEvents)
            {
                PublishEvent(e, c, aggRoot.AggregateId);
            }
        }

        public T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot
        {
            if (aggregateId == Guid.Empty)
            {
                throw new ArgumentException(string.Format("Invalid Aggregate ID ({0})", aggregateId.ToString()));
            }

            T result = System.Activator.CreateInstance<T>();

            // TODO: Need to introduce snapshot functionality
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
            StringReader reader = new StringReader((string)row["EventData"]);
            XmlReader xr = XmlReader.Create(reader);
            
            Type eventType = Type.GetType((string)row["EventType"], true);

            DataContractSerializer ser = new DataContractSerializer(eventType);
            return (IEvent)ser.ReadObject(xr);
        }
    }
}
