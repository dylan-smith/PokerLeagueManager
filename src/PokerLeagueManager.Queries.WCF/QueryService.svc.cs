using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return (IDataTransferObject)ExecuteQuery(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return (int)ExecuteQuery(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return (IEnumerable<IDataTransferObject>)ExecuteQuery(query);
        }

        private object ExecuteQuery(IQuery query)
        {
            Resolver.Container.RegisterInstance<OperationContext>(OperationContext.Current);

            var queryHandlerFactory = Resolver.Container.Resolve<IQueryHandlerFactory>();
            var queryFactory = Resolver.Container.Resolve<IQueryFactory>();
            var result = queryHandlerFactory.ExecuteQuery(queryFactory.Create(query));
            return result;
        }
    }
}
