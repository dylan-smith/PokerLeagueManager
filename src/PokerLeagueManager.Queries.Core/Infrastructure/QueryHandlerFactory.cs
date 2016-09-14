using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryHandlerFactory : IQueryHandlerFactory, IQueryService
    {
        private IQueryDataStore _queryDataStore;

        public QueryHandlerFactory(IQueryDataStore queryDataStore)
        {
            _queryDataStore = queryDataStore;
        }

        public TResult ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException("query", "Cannot execute a null Query.");
            }

            return FindQueryHandler<TQuery, TResult>().Execute(query);
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "This Exception should never happen, so I'm ok with leaving it as-is")]
        public TResult ExecuteQuery<TResult>(IQuery query)
        {
            var executeQueryMethod = from m in typeof(QueryHandlerFactory).GetMethods()
                                       where m.Name == "ExecuteQuery" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 2
                                       select m;

            if (executeQueryMethod.Count() != 1)
            {
                throw new Exception("Unexpected Exception. Could not find the ExecuteQuery method via Reflection.");
            }

            MethodInfo generic = executeQueryMethod.First().MakeGenericMethod(query.GetType(), typeof(TResult));

            try
            {
                return (TResult)generic.Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return ExecuteQuery<IDataTransferObject>(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return ExecuteQuery<IEnumerable<IDataTransferObject>>(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return ExecuteQuery<int>(query);
        }

        private IHandlesQuery<TQuery, TResult> FindQueryHandler<TQuery, TResult>()
            where TQuery : IQuery
        {
            var matchingTypes = typeof(IHandlesQuery<,>).FindHandlers<TQuery>(Assembly.GetExecutingAssembly());

            if (matchingTypes.Count() == 0)
            {
                throw new ArgumentException(string.Format("Could not find Query Handler for {0}", typeof(TQuery).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Query Handler for {0}", typeof(TQuery).Name));
            }

            var result = (IHandlesQuery<TQuery, TResult>)UnitySingleton.Container.Resolve(matchingTypes.First(), null);
            result.Repository = _queryDataStore;

            return result;
        }
    }
}
