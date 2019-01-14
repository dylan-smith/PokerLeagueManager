using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class PlayerDeletedEvent : BaseEvent
    {
        public Guid PlayerId
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
