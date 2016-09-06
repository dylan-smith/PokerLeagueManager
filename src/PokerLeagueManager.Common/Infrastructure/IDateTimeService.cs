using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IDateTimeService
    {
        DateTime Now();

        DateTime UtcNow();
    }
}
