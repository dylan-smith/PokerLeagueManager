using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Common.DTO.Infrastructure
{
    public class Bootstrapper : BaseBootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                Container.RegisterType<IDtoFactory, DtoFactory>();

                PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}