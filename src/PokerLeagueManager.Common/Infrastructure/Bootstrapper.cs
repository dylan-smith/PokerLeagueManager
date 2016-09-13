using System.Security.Principal;
using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Common.Infrastructure
{
    public static class Bootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                UnitySingleton.Container.RegisterType<IDateTimeService, DateTimeService>();
                UnitySingleton.Container.RegisterType<IGuidService, GuidService>();
                UnitySingleton.Container.RegisterType<IDatabaseLayer, SqlServerDatabaseLayer>();
                UnitySingleton.Container.RegisterInstance<IIdentity>(System.Security.Principal.WindowsPrincipal.Current.Identity);

                UnitySingleton.Container.RegisterType<ICommandFactory, CommandFactory>();
                UnitySingleton.Container.RegisterType<ICommandService, CommandServiceProxy>();

                UnitySingleton.Container.RegisterType<IQueryFactory, QueryFactory>();
                UnitySingleton.Container.RegisterType<IQueryService, QueryServiceProxy>();

                UnitySingleton.Container.RegisterType<IDtoFactory, DtoFactory>();

                _hasBootstrapped = true;
            }
        }
    }
}