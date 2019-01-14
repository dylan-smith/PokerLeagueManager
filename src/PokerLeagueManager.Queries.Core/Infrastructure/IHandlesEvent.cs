using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IHandlesEvent<in T>
        where T : IEvent
    {
        IQueryDataStore QueryDataStore { get; set; }

        void Handle(T e);
    }
}
