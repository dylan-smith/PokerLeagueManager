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

        public object ExecuteQuery<T>(T query)
            where T : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException("query", "Cannot execute a null Query.");
            }

            return FindQueryHandler<T>().Execute(query);
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "This Exception should never happen, so I'm ok with leaving it as-is")]
        public object ExecuteQuery(IQuery query)
        {
            var executeQueryMethod = from m in typeof(QueryHandlerFactory).GetMethods()
                                       where m.Name == "ExecuteQuery" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition
                                       select m;

            if (executeQueryMethod.Count() != 1)
            {
                throw new Exception("Unexpected Exception. Could not find the ExecuteQuery method via Reflection.");
            }

            MethodInfo generic = executeQueryMethod.First().MakeGenericMethod(query.GetType());

            try
            {
                return generic.Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return (IDataTransferObject)ExecuteQuery(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return (IEnumerable<IDataTransferObject>)ExecuteQuery(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return (int)ExecuteQuery(query);
        }

        private IHandlesQuery<T> FindQueryHandler<T>()
            where T : IQuery
        {
            var matchingTypes = typeof(IHandlesQuery<>).FindHandlers<T>(Assembly.GetExecutingAssembly());

            if (matchingTypes.Count() == 0)
            {
                throw new ArgumentException(string.Format("Could not find Query Handler for {0}", typeof(T).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Query Handler for {0}", typeof(T).Name));
            }

            var result = (IHandlesQuery<T>)UnitySingleton.Container.Resolve(matchingTypes.First(), null);
            result.Repository = _queryDataStore;

            return result;
        }
    }
}
