using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerLeagueManager.Queries.WCF.Infrastructure
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
            PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

            _hasBootstrapped = true;
        }
    }
}