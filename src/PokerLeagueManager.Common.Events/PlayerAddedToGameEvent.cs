using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Events
{
    [DataContract]
    public class PlayerAddedToGameEvent : BaseEvent
    {
        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int Placing { get; set; }

        [DataMember]
        public int Winnings { get; set; }
    }
}
