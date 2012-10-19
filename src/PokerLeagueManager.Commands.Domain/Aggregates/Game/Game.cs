using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Domain.Aggregates.Game
{
    public class Game : BaseAggregateRoot
    {
        private DateTime _gameDate;
        private List<Player> _players = new List<Player>();

        public Game(Guid gameId, DateTime gameDate)
        {
            if (gameId != Guid.Empty)
            {
                this.PublishEvent(new GameCreatedEvent() { AggregateId = gameId, GameDate = gameDate });
            }
            else
            {
                this.PublishEvent(new GameCreatedEvent() { AggregateId = Guid.NewGuid(), GameDate = gameDate });
            }
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

            if (_players.Any(x => x.PlayerName.Trim() == playerName.Trim()))
            {
                throw new ArgumentException("Cannot add the same Player to a Game more than once", "playerName");
            }

            this.PublishEvent(new PlayerAddedToGameEvent() { AggregateId = AggregateId, PlayerName = playerName, Placing = placing, Winnings = winnings });
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
        }

        public void ApplyEvent(GameCreatedEvent e)
        {
            AggregateId = e.AggregateId;
            _gameDate = e.GameDate;
        }

        public void ApplyEvent(PlayerAddedToGameEvent e)
        {
            _players.Add(new Player(e.PlayerName, e.Placing, e.Winnings));
        }
    }
}
