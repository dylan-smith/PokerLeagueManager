using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class GameCompletedEvent : BaseEvent
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

        [DataMember]
        public IDictionary<Guid, int> Placings { get; } = new Dictionary<Guid, int>();
    }
}
