using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Common.Utilities
{
    public abstract class BaseBootstrapper
    {
        public static IUnityContainer Container
        {
            get { return UnityHelper.Container; }
        }
    }
}