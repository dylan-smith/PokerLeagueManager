﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Utilities.Exceptions;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Game : BaseAggregateRoot
    {
        private List<Player> _players = new List<Player>();

        public Game(Guid gameId, DateTime gameDate)
        {
            if (gameDate == DateTime.MinValue || gameDate == DateTime.MaxValue || gameDate == default(DateTime))
            {
                throw new ArgumentException("Invalid GameDate", "gameDate");
            }

            gameId = gameId == Guid.Empty ? Guid.NewGuid() : gameId;

            this.PublishEvent(new GameCreatedEvent() { AggregateId = gameId, GameDate = gameDate });
        }

        private Game()
        {
        }

        public void AddPlayer(string playerName, int placing, int winnings)
        {
            if (winnings < 0)
            {
                throw new ArgumentException("Winnings cannot be negative", "winnings");
            }

            if (placing <= 0)
            {
                throw new ArgumentException("Placing must be greater than 0", "placing");
            }

            if (string.IsNullOrWhiteSpace(playerName))
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
            if (_players.Count < 2)
            {
                throw new GameWithNotEnoughPlayersException("Each game must have at least 2 players");
            }

            var orderedPlayers = _players.OrderBy(x => x.Placing);

            var curPlacing = 1;

            foreach (var curPlayer in orderedPlayers)
            {
                if (curPlayer.Placing != curPlacing++)
                {
                    throw new PlayerPlacingsNotInOrderException("The player placings must start at one and have no duplicates and not be higher than the total # of players");
                }
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Plumbing needs this method signature to exist to work properly")]
        private void ApplyEvent(GameCreatedEvent e)
        {
            AggregateId = e.AggregateId;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(PlayerAddedToGameEvent e)
        {
            _players.Add(new Player(e.PlayerName, e.Placing, e.Winnings));
        }
    }
}
