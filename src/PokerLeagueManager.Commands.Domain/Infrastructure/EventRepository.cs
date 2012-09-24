using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
    }
}
