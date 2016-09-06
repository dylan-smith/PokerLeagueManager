using System;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.Infrastructure
{
    public class EventCommandPair
    {
        public EventCommandPair(IEvent e, ICommand c, Guid aggregateId)
        {
            Event = e;
            Command = c;
            AggregateId = aggregateId;
        }

        public IEvent Event { get; set; }

        public ICommand Command { get; set; }

        public Guid AggregateId { get; set; }
    }
}
