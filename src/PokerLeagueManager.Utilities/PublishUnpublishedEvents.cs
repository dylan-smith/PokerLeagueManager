using PokerLeagueManager.Commands.Domain.Infrastructure;
using Unity;

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
