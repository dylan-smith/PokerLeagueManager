using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.UI.WPF.ViewModels;

namespace PokerLeagueManager.UI.WPF
{
    public class Resolver
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

                return UnityHelper.Container;
            }
        }

        public static void Bootstrap()
        {
            UnityHelper.Container.RegisterType<IEnterGameResultsViewModel, EnterGameResultsViewModel>();

            PokerLeagueManager.Common.DTO.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Common.Commands.Infrastructure.Bootstrapper.Bootstrap();
            PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

            _hasBootstrapped = true;
        }
    }
}
