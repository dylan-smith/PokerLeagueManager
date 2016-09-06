using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Infrastructure
{
    [DataContract]
    public class BaseEvent : IEvent
    {
        public BaseEvent()
        {
            EventId = Guid.NewGuid();
            Timestamp = DateTime.Now;
            CommandId = Guid.Empty;
            AggregateId = Guid.Empty;
        }

        [DataMember]
        public Guid EventId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public Guid CommandId { get; set; }

        [DataMember]
        public Guid AggregateId { get; set; }
    }
}
