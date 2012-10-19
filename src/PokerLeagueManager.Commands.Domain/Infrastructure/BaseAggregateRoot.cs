using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public abstract class BaseAggregateRoot : IAggregateRoot
    {
        private ICollection<IEvent> _pendingEvents = new List<IEvent>();

        protected BaseAggregateRoot()
        {
            AggregateId = Guid.Empty;
            EventsApplied = false;
        }

        public virtual ICollection<IEvent> PendingEvents
        {
            get { return _pendingEvents; }
        }

        public bool EventsApplied { get; set; }

        public Guid AggregateId { get; set; }

        public void ApplyEvent(IEvent e)
        {
            InvokeEventHandler(FindEventHandler(e.GetType()), e);
        }

        protected void PublishEvent(IEvent e)
        {
            _pendingEvents.Add(e);
            ApplyEvent(e);
        }

        protected MethodInfo FindEventHandler(Type eventType)
        {
            var matchingMethods = from m in this.GetType().GetMethods()
                                  where m.Name == "ApplyEvent" && m.GetParameters().Count() == 1 && m.GetParameters()[0].ParameterType == eventType
                                  select m;

            return matchingMethods.FirstOrDefault();
        }

        protected void InvokeEventHandler(MethodInfo methodtoBeInvoked, IEvent e)
        {
            if (methodtoBeInvoked != null)
            {
                methodtoBeInvoked.Invoke(this, new object[] { e });
            }
        }
    }
}
