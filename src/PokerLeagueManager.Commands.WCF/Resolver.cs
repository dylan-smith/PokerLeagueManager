﻿using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.WCF
{
    public static class Resolver
    {
        private static bool _hasBootstrapped = false;

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
            PokerLeagueManager.Commands.Domain.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Common.Infrastructure.Bootstrapper.Bootstrap();

            _hasBootstrapped = true;
        }
    }
}