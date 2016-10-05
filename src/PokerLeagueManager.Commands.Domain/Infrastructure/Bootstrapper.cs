using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public static class Bootstrapper
    {
        private static bool _hasBootstrapped = false;

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Unity recommended pattern")]
        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                UnitySingleton.Container.RegisterType<ICommandHandlerFactory, CommandHandlerFactory>();
                UnitySingleton.Container.RegisterType<IEventService, EventServiceProxy>();
                UnitySingleton.Container.RegisterType<IEventRepository, EventRepository>();
                UnitySingleton.Container.RegisterType<IEventServiceProxyFactory, EventServiceProxyFactory>();
                UnitySingleton.Container.RegisterType<IEventServiceProxy, EventServiceProxy>(new ContainerControlledLifetimeManager());
                UnitySingleton.Container.RegisterType<ICommandRepository, CommandRepository>();

                PokerLeagueManager.Common.Infrastructure.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}