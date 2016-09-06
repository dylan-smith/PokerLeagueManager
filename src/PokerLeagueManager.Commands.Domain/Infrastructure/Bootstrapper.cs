using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public static class Bootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                UnitySingleton.Container.RegisterType<ICommandHandlerFactory, CommandHandlerFactory>();
                UnitySingleton.Container.RegisterType<IEventService, EventServiceProxy>();
                UnitySingleton.Container.RegisterType<IEventRepository, EventRepository>();
                UnitySingleton.Container.RegisterType<IEventServiceProxyFactory, EventServiceProxyFactory>();
                UnitySingleton.Container.RegisterType<ICommandRepository, CommandRepository>();

                PokerLeagueManager.Common.Infrastructure.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}