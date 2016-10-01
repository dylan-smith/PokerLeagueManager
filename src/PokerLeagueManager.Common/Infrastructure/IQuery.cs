using System;
using System.Diagnostics.CodeAnalysis;

namespace PokerLeagueManager.Common.Infrastructure
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Needed so Query objects can identify the return type of that query")]
    public interface IQuery<TResult> : IQuery
    {
    }

    public interface IQuery
    {
        Guid QueryId { get; set; }

        DateTime Timestamp { get; set; }

        string IPAddress { get; set; }
    }
}
