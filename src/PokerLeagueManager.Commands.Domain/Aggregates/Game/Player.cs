using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Player
    {
        public Player(string playerName, int placing, int winnings)
        {
            PlayerName = playerName;
            Placing = placing;
            Winnings = winnings;
        }

        public string PlayerName { get; set; }

        public int Placing { get; set; }

        public int Winnings { get; set; }
    }
}
