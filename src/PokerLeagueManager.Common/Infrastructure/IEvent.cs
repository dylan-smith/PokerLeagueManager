using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IEvent
    {
        Guid EventId { get; set; }

        DateTime Timestamp { get; set; }

        Guid CommandId { get; set; }

        Guid AggregateId { get; set; }
    }
}
