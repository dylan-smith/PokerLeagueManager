using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Game : BaseAggregateRoot
    {
        private readonly List<Guid> _players = new List<Guid>();
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

            if (_players.Any(p => p == player.PlayerId))
            {
                throw new DuplicatePlayerAddedToGameException(player.PlayerId, base.AggregateId);
            }

            base.PublishEvent(new PlayerAddedToGameEvent() { GameId = base.AggregateId, PlayerId = player.PlayerId });
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
            _players.Add(e.PlayerId);
        }
    }
}
