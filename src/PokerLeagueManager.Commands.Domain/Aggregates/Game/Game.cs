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
            if (winnings < 0)
            {
                throw new ArgumentException("winnings cannot be negative", "winnings");
            }

            if (placing <= 0)
            {
                throw new ArgumentException("placing must be greater than 0", "placing");
            }

            if (string.IsNullOrEmpty(playerName))
            {
                throw new ArgumentException("Player Name must be entered", "playerName");
            }

            this.PublishEvent(new PlayerAddedToGameEvent() { PlayerName = playerName, Placing = placing, Winnings = winnings });
        }

        public void ValidateGame()
        {
            var orderedPlayers = _players.OrderBy(x => x.Placing);

            var curPlacing = 1;

            foreach (var curPlayer in orderedPlayers)
            {
                if (curPlayer.Placing != curPlacing++)
                {
                    throw new Exception("The player placings must start at one and have no duplicates and not be higher than the total # of players");
                }
            }

            // TODO: validate that the total winnings equals the total pay-in (once pay-in has been implemented)
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
