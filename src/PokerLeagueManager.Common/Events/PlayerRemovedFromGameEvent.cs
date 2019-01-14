using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class PlayerRemovedFromGameEvent : BaseEvent
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
        public Guid PlayerId { get; set; }
    }
}
