using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryHandlerFactory : IQueryService
    {
        TResult Execute<TResult>(IQuery query);
    }
}
