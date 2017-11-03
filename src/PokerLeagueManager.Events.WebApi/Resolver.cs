using PokerLeagueManager.Common.Infrastructure;
using Unity;

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