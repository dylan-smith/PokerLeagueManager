using Microsoft.Practices.Unity;
using PokerLeagueManager.Commands.Domain.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class PublishUnpublishedEvents
    {
        public static void Publish()
        {
            Resolver.Container.Resolve<IEventRepository>().PublishAllUnpublishedEvents();
        }
    }
}
