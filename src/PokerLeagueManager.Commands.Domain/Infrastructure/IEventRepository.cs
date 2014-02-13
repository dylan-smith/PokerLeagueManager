using System;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventRepository
    {
        void PublishEvents(IAggregateRoot aggRoot, ICommand c);

        void PublishEvents(IAggregateRoot aggRoot, ICommand c, Guid originalVersion);

        T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot;

        bool DoesAggregateExist(Guid aggregateId);

        void PublishAllUnpublishedEvents();
    }
}
