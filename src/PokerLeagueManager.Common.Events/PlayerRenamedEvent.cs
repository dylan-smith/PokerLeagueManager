using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class PlayerRenamedEvent : BaseEvent
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
        public string OldPlayerName { get; set; }

        [DataMember]
        public string NewPlayerName { get; set; }
    }
}
