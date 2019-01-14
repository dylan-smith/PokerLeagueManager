using PokerLeagueManager.Common.Infrastructure;
using Unity;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public static class Bootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                UnitySingleton.Container.RegisterType<IEventHandlerFactory, EventHandlerFactory>();
                UnitySingleton.Container.RegisterType<IQueryDataStore, QueryDataStore>();
                UnitySingleton.Container.RegisterType<IQueryHandlerFactory, QueryHandlerFactory>();
                UnitySingleton.Container.RegisterType<IIdempotencyChecker, IdempotencyChecker>();

                PokerLeagueManager.Common.Infrastructure.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}