using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IQuery
    {
        Guid QueryId { get; set; }

        DateTime Timestamp { get; set; }

        string User { get; set; }

        string IPAddress { get; set; }
    }
}
