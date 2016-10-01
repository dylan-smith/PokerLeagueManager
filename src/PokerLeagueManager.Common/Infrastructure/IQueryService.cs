using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IQueryService
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}