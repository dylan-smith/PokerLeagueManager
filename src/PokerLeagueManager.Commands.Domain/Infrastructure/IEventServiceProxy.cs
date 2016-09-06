using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventServiceProxy : IEventService
    {
        string ServiceUrl { get; set; }
    }
}
