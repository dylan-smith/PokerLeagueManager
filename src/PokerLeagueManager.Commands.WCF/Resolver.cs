using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerLeagueManager.Commands.WCF
{
    public class Resolver
    {
        public static IUnityContainer Container
        {
            get
            {
                if (!_hasBootstrapped)
                {
                    Bootstrap();
                }

                return UnityHelper.Container;
            }
        }

        private static bool _hasBootstrapped;

        public static void Bootstrap()
        {
            PokerLeagueManager.Commands.Domain.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Common.Commands.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

            _hasBootstrapped = true;
        }
    }
}