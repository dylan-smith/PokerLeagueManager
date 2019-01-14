using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IHandlesQuery<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
    {
        IQueryDataStore Repository { get; set; }

        TResult Execute(TQuery query);
    }
}
