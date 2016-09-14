using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryHandlerFactory
    {
        TResult ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;

        TResult ExecuteQuery<TResult>(IQuery query);
    }
}
