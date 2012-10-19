using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.Infrastructure
{
    public class FakeEventRepository : IEventRepository
    {
        public FakeEventRepository()
        {
            EventCommandList = new List<EventCommandPair>();
            EventList = new List<IEvent>();
            InitialEvents = new List<IEvent>();
        }

        public IList<EventCommandPair> EventCommandList { get; set; }

        public IList<IEvent> EventList { get; set; }

        public IEnumerable<IEvent> InitialEvents { get; set; }

        public void PublishEvent(IEvent e, ICommand c, Guid aggregateId)
        {
            EventCommandList.Add(new EventCommandPair(e, c, aggregateId));
            EventList.Add(e);
        }

        public void PublishEvents(IAggregateRoot aggRoot, ICommand c)
        {
            foreach (IEvent e in aggRoot.PendingEvents)
            {
                EventCommandList.Add(new EventCommandPair(e, c, aggRoot.AggregateId));
                EventList.Add(e);
            }
        }

        public T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot
        {
            T aggRootInstance = default(T);

            if (InitialEvents != null)
            {
                var aggEvents = InitialEvents.Where<IEvent>(e => e.AggregateId == aggregateId);

                if (aggEvents.Count() > 0)
                {
                    aggRootInstance = (T)System.Activator.CreateInstance<T>();

                    aggRootInstance.AggregateId = aggregateId;

                    foreach (IEvent e in aggEvents)
                    {
                        aggRootInstance.ApplyEvent(e);
                    }
                }
            }

            return aggRootInstance;
        }

        public bool DoesAggregateExist(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty || InitialEvents == null)
            {
                return false;
            }

            return InitialEvents.Any(e => e.AggregateId == aggregateId);
        }
    }
}
