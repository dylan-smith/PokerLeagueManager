using System;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IQueryDataStore _queryDataStore;

        public QueryHandlerFactory(IQueryDataStore queryDataStore)
        {
            _queryDataStore = queryDataStore;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            return Execute<TResult>((IQuery)query);
        }

        public TResult Execute<TResult>(IQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Cannot execute a null Query.");
            }

            var methods = typeof(QueryHandlerFactory).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(m => m.Name == "ExecuteQueryHandler" &&
                                        m.ContainsGenericParameters &&
                                        m.IsGenericMethod &&
                                        m.IsGenericMethodDefinition &&
                                        m.GetGenericArguments().Length == 2);

            if (methods.Count() != 1)
            {
                throw new InvalidOperationException("Unexpected Exception. Could not find the ExecuteQueryHandler method via Reflection.");
            }

            var executeQueryHandlerMethod = methods.First();
            var queryType = query.GetType();
            var genericInterface = queryType.GetInterfaces().First(i => i.IsGenericType);
            var queryReturnType = genericInterface.GenericTypeArguments[0];

            var generic = executeQueryHandlerMethod.MakeGenericMethod(queryType, queryReturnType);

            try
            {
                return (TResult)generic.Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            }
        }

        public TResult ExecuteQueryHandler<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var matchingTypes = typeof(IHandlesQuery<,>).FindHandlers<TQuery>(Assembly.GetExecutingAssembly());

            if (!matchingTypes.Any())
            {
                throw new ArgumentException(string.Format("Could not find Query Handler for {0}", typeof(TQuery).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Query Handler for {0}", typeof(TQuery).Name));
            }

            var queryHandlerType = matchingTypes.First();
            var handler = UnitySingleton.Container.Resolve(queryHandlerType, null);

            var repoProperty = queryHandlerType.GetProperty("Repository");
            var executeMethod = queryHandlerType.GetMethod("Execute");

            repoProperty.SetValue(handler, _queryDataStore);
            return (TResult)executeMethod.Invoke(handler, new object[] { query });
        }
    }
}
