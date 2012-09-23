using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Aggregates.Game
{
    public class Game : BaseAggregateRoot
    {
        private DateTime _gameDate;
        private List<Player> _players = new List<Player>();

        public Game(DateTime gameDate)
        {
            this.PublishEvent(new GameCreatedEvent() { GameDate = gameDate });
        }

        public void AddPlayer(string playerName, int placing, int winnings)
        {
            this.PublishEvent(new PlayerAddedToGameEvent() { PlayerName = playerName, Placing = placing, Winnings = winnings });
        }

        // TODO: Can these be private or protected instead?
        public void ApplyEvent(GameCreatedEvent e)
        {
            _gameDate = e.GameDate;
        }

        public void ApplyEvent(PlayerAddedToGameEvent e)
        {
            _players.Add(new Player(e.PlayerName, e.Placing, e.Winnings));
        }
    }
}
