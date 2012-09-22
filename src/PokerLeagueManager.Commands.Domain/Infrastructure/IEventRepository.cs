using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventRepository
    {
        void PublishEvent(IEvent domainEvent, ICommand command);

        void PublishEvents(IEnumerable<IEvent> domainEvents, ICommand command);

        T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot;
    }
}
