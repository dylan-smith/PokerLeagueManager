using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IEventService
    {
        void HandleEvent(IEvent e);
    }
}
