namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IQueryService
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}