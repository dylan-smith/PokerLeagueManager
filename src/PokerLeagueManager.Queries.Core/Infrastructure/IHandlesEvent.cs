using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IHandlesEvent<T>
        where T : IEvent
    {
        IQueryDataStore QueryDataStore { get; set; }

        void Handle(T e);
    }
}
