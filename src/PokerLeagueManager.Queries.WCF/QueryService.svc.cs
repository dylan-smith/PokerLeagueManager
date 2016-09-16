using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            return ExecuteQuery<TResult>(query);
        }

        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return ExecuteQuery<IDataTransferObject>(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return ExecuteQuery<int>(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return ExecuteQuery<IEnumerable<IDataTransferObject>>(query);
        }

        private TResult ExecuteQuery<TResult>(IQuery query)
        {
            Resolver.Container.RegisterInstance<OperationContext>(OperationContext.Current);

            var queryHandlerFactory = Resolver.Container.Resolve<IQueryHandlerFactory>();
            var queryFactory = Resolver.Container.Resolve<IQueryFactory>();
            var result = queryHandlerFactory.Execute<TResult>(queryFactory.Create(query));
            return result;
        }
    }
}
