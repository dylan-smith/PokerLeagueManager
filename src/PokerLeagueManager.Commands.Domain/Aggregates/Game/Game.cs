using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Game : BaseAggregateRoot
    {
        private readonly Dictionary<Guid, int> _players = new Dictionary<Guid, int>();
        private bool _deleted = false;

        public Game(Guid gameId, DateTime gameDate)
        {
            if (gameDate == DateTime.MinValue || gameDate == DateTime.MaxValue || gameDate == default(DateTime))
            {
                throw new InvalidGameDateException(gameDate);
            }

            gameId = gameId == Guid.Empty ? Guid.NewGuid() : gameId;

            PublishEvent(new GameCreatedEvent() { GameId = gameId, GameDate = gameDate });
        }

        private Game()
        {
        }

        public void DeleteGame()
        {
            if (_deleted)
            {
                throw new GameAlreadyDeletedException(base.AggregateId);
            }

            base.PublishEvent(new GameDeletedEvent() { GameId = base.AggregateId });
        }

        public void AddPlayerToGame(Player player)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (player.IsDeleted())
            {
                throw new PlayerDeletedException(player.PlayerId);
            }

            if (_players.ContainsKey(player.PlayerId))
            {
                throw new DuplicatePlayerAddedToGameException(player.PlayerId, base.AggregateId);
            }

            base.PublishEvent(new PlayerAddedToGameEvent() { GameId = base.AggregateId, PlayerId = player.PlayerId });
        }

        public void RemovePlayerFromGame(Guid playerId)
        {
            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            base.PublishEvent(new PlayerRemovedFromGameEvent() { GameId = base.AggregateId, PlayerId = playerId });
        }

        public void AddRebuy(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            base.PublishEvent(new RebuyAddedEvent() { GameId = base.AggregateId, PlayerId = playerId });
        }

        public void RemoveRebuy(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            if (_players[playerId] <= 0)
            {
                throw new NoRebuysLeftToRemoveException(base.AggregateId, playerId);
            }

            base.PublishEvent(new RebuyRemovedEvent() { GameId = base.AggregateId, PlayerId = playerId });
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Plumbing needs this method signature to exist to work properly")]
        private void ApplyEvent(GameCreatedEvent e)
        {
            base.AggregateId = e.GameId;
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Plumbing needs this method signature to exist to work properly")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(GameDeletedEvent e)
        {
            _deleted = true;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(PlayerAddedToGameEvent e)
        {
            _players.Add(e.PlayerId, 0);
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(RebuyAddedEvent e)
        {
            _players[e.PlayerId]++;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(RebuyRemovedEvent e)
        {
            _players[e.PlayerId]--;
        }
    }
}
