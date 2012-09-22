using PokerLeagueManager.Commands.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Aggregates.Game
{
    public class Game : BaseAggregateRoot
    {
        public Game(DateTime gameDateTime)
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(string playerName, int placing, int winnings)
        {
            throw new NotImplementedException();
        }
    }
}
