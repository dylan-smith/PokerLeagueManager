using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Common.Infrastructure
{
    public static class Bootstrapper
    {
        private static bool _hasBootstrapped = false;

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Unity prescribed pattern")]
        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                UnitySingleton.Container.RegisterType<IDateTimeService, DateTimeService>();
                UnitySingleton.Container.RegisterType<IGuidService, GuidService>();
                UnitySingleton.Container.RegisterType<IDatabaseLayer, SqlServerDatabaseLayer>();
                UnitySingleton.Container.RegisterInstance<IIdentity>(System.Security.Principal.WindowsPrincipal.Current.Identity);

                UnitySingleton.Container.RegisterType<ICommandFactory, CommandFactory>();
                UnitySingleton.Container.RegisterType<ICommandService, CommandServiceProxy>(new ContainerControlledLifetimeManager());

                UnitySingleton.Container.RegisterType<IQueryFactory, QueryFactory>();
                UnitySingleton.Container.RegisterType<IQueryService, QueryServiceProxy>(new ContainerControlledLifetimeManager());

                UnitySingleton.Container.RegisterType<IDtoFactory, DtoFactory>();

                _hasBootstrapped = true;
            }
        }
    }
}