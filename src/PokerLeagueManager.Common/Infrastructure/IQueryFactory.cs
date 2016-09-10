namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IQueryFactory
    {
        T Create<T>()
            where T : IQuery, new();

        T Create<T>(T query)
            where T : IQuery;
    }
}
