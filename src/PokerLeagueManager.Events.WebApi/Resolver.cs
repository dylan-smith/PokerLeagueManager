﻿using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Events.WebApi
{
    public static class Resolver
    {
        private static bool _hasBootstrapped;

        public static IUnityContainer Container
        {
            get
            {
                if (!_hasBootstrapped)
                {
                    Bootstrap();
                }

                return UnitySingleton.Container;
            }
        }

        public static void Bootstrap()
        {
            PokerLeagueManager.Common.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Queries.Core.Infrastructure.Bootstrapper.Bootstrap();

            _hasBootstrapped = true;
        }
    }
}