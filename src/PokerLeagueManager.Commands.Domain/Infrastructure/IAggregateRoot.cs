using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IAggregateRoot
    {
        ICollection<IEvent> PendingEvents { get; }

        Guid AggregateId { get; set; }

        bool EventsApplied { get; set; }

        void ApplyEvent(IEvent e);
    }
}
