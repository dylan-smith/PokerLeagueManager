using System.Security.Principal;
using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Common.Utilities
{
    public class Bootstrapper : BaseBootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                Container.RegisterType<IDateTimeService, DateTimeService>();
                Container.RegisterType<IGuidService, GuidService>();
                Container.RegisterType<IDatabaseLayer, SqlServerDatabaseLayer>();
                Container.RegisterInstance<IIdentity>(System.Security.Principal.WindowsPrincipal.Current.Identity);

                _hasBootstrapped = true;
            }
        }
    }
}