using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class GameDeletedEvent : BaseEvent
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
    }
}
