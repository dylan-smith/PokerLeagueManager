using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        IDatabaseLayer _databaseLayer;

        public EventRepository(IDatabaseLayer databaseLayer)
        {
            _databaseLayer = databaseLayer;
        }

        public void PublishEvent(IEvent e, ICommand c)
        {
            throw new NotImplementedException();
        }

        public void PublishEvents(IEnumerable<IEvent> events, ICommand c)
        {
            // TODO: make sure this is all in a transaction
            foreach (var e in events)
            {
                PublishEvent(e, c);
            }
        }

        public T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
