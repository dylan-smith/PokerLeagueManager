using PokerLeagueManager.Common.Infrastructure;
using Unity;

namespace PokerLeagueManager.Utilities
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