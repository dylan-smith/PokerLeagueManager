using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class PayoutsCalculatedEvent : BaseEvent
    {
        public Guid GameId
        {
            get
            {
                return base.AggregateId;
            }

            set
            {
                base.AggregateId = value;
            }
        }

        [DataMember]
        public int First { get; set; }

        [DataMember]
        public int Second { get; set; }

        [DataMember]
        public int Third { get; set; }
    }
}
