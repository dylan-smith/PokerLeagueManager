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
        void PublishEvent(IEvent e, ICommand c, Guid aggregateId);

        void PublishEvents(IAggregateRoot aggRoot, ICommand c);

        T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot;

        bool DoesAggregateExist(Guid aggregateId);
    }
}
