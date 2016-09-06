using System;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IIdempotencyChecker
    {
        IDatabaseLayer DatabaseLayer { get; set; }

        bool CheckIdempotency(Guid eventId);

        void MarkEventAsProcessed(Guid eventId);
    }
}
