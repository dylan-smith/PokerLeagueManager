using PokerLeagueManager.Common.Commands.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class EnterGameResults : BaseCommand
    {
        [DataMember]
        public DateTime GameDate { get; set; }
        
        [DataMember]
        public IEnumerable<GamePlayer> Players { get; set; }

        [DataContract]
        public class GamePlayer
        {
            [DataMember]
            public string PlayerName { get; set; }
            
            [DataMember]
            public int Placing { get; set; }
            
            [DataMember]
            public int Winnings { get; set; }
        }
    }
}
