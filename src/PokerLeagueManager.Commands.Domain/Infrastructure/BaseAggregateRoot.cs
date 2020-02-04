using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public abstract class BaseAggregateRoot : IAggregateRoot
    {
        private readonly ICollection<IEvent> _pendingEvents = new List<IEvent>();

        protected BaseAggregateRoot()
        {
            AggregateId = Guid.Empty;
            AggregateVersion = Guid.Empty;
        }

        public ICollection<IEvent> PendingEvents
        {
            get { return _pendingEvents; }
        }

        public Guid AggregateId { get; set; }

        public Guid AggregateVersion { get; set; }

        public void ApplyEvent(IEvent e)
        {
            InvokeEventHandler(FindEventHandler(e.GetType()), e);
        }

        protected void PublishEvent(IEvent e)
        {
            _pendingEvents.Add(e);
            ApplyEvent(e);
        }

        [SuppressMessage("SonarAnalyzer.CSharp", "S3011", Justification = "Plumbing magic")]
        private MethodInfo FindEventHandler(Type eventType)
        {
            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(m => m.Name == "ApplyEvent" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == eventType);

            return methods.FirstOrDefault();
        }

        private void InvokeEventHandler(MethodInfo methodtoBeInvoked, IEvent e)
        {
            if (methodtoBeInvoked != null)
            {
                methodtoBeInvoked.Invoke(this, new object[] { e });
            }
        }
    }
}
