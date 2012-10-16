using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Commands.Domain.ServiceProxies;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class Bootstrapper : BaseBootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                Container.RegisterType<ICommandHandlerFactory, CommandHandlerFactory>();
                Container.RegisterType<IEventSubscriberFactory, EventSubscriberFactory>();
                Container.RegisterType<IEventService, EventServiceProxy>();
                Container.RegisterType<IEventRepository, EventRepository>();
                Container.RegisterType<IEventServiceProxyFactory, EventServiceProxyFactory>();
                Container.RegisterType<IQueryService, QueryServiceProxy>();

                PokerLeagueManager.Common.Commands.Infrastructure.Bootstrapper.Bootstrap();
                PokerLeagueManager.Common.Events.Infrastructure.Bootstrapper.Bootstrap();
                PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}