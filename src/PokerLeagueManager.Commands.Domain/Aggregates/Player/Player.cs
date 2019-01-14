using System;
using System.Diagnostics.CodeAnalysis;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Player : BaseAggregateRoot
    {
        private bool _deleted = false;

        public Player(Guid playerId, string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new InvalidPlayerNameException(playerName);
            }

            playerId = playerId == Guid.Empty ? Guid.NewGuid() : playerId;

            PublishEvent(new PlayerCreatedEvent() { PlayerId = playerId, PlayerName = playerName });
        }

        private Player()
        {
        }

        public Guid PlayerId
        {
            get => base.AggregateId;
        }

        public void DeletePlayer()
        {
            if (_deleted)
            {
                throw new PlayerAlreadyDeletedException(base.AggregateId);
            }

            base.PublishEvent(new PlayerDeletedEvent() { PlayerId = base.AggregateId });
        }

        public bool IsDeleted() => _deleted;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Plumbing needs this method signature to exist to work properly")]
        private void ApplyEvent(PlayerCreatedEvent e)
        {
            base.AggregateId = e.PlayerId;
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Plumbing needs this method signature to exist to work properly")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Is called via reflection")]
        private void ApplyEvent(PlayerDeletedEvent e)
        {
            _deleted = true;
        }
    }
}
