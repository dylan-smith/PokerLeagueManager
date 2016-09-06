using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface ICommand
    {
        Guid CommandId { get; set; }

        DateTime Timestamp { get; set; }

        string User { get; set; }

        string IPAddress { get; set; }
    }
}
