using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.DTO;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class Bootstrapper : BaseBootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                Container.RegisterType<IEventHandlerFactory, EventHandlerFactory>();
                Container.RegisterType<IQueryDataStore, QueryDataStore>();
                Container.RegisterType<IQueryService, QueryHandler>();

                PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}