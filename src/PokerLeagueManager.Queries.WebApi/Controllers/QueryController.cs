using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;
using Unity;

namespace PokerLeagueManager.Queries.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QueryController : ApiController, IQueryService
    {
        public QueryController()
        {
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            return ExecuteQuery<TResult>(query);
        }

        public HttpResponseMessage Post(string queryName, [FromBody]JToken jsonbody)
        {
            var queryType = GetQueryType(queryName);
            var queryReturnType = GetQueryReturnType(queryType);

            var query = (IQuery)Activator.CreateInstance(queryType);

            if (jsonbody != null)
            {
                query = (IQuery)JsonConvert.DeserializeObject(jsonbody.ToString(), queryType);
            }

            var ai = new TelemetryClient();
            ai.TrackEvent(queryType.Name, query.GetPropertiesDictionary());

            var result = ExecuteQuery(query, queryReturnType);

            return CreateResponse(queryReturnType, HttpStatusCode.OK, result);
        }

        private HttpResponseMessage CreateResponse(Type queryReturnType, HttpStatusCode statusCode, object result)
        {
            var extensionMethods = typeof(HttpRequestMessage).GetExtensionMethods(typeof(System.Web.Http.ApiController).Assembly);
            var createResponseMethod = extensionMethods.Single(x => x.Name == "CreateResponse" &&
                                                                    x.ContainsGenericParameters &&
                                                                    x.IsGenericMethod &&
                                                                    x.IsGenericMethodDefinition &&
                                                                    x.GetGenericArguments().Count() == 1 &&
                                                                    x.GetParameters().Count() == 3);

            var createResponseGeneric = createResponseMethod.MakeGenericMethod(queryReturnType);

            try
            {
                return (HttpResponseMessage)createResponseGeneric.Invoke(Request, new object[] { Request, statusCode, result });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private object ExecuteQuery(IQuery query, Type queryReturnType)
        {
            var executeQueryMethods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                               .Where(m => m.Name == "ExecuteQuery" &&
                                                           m.ContainsGenericParameters &&
                                                           m.IsGenericMethod &&
                                                           m.IsGenericMethodDefinition &&
                                                           m.GetGenericArguments().Count() == 1);

            if (executeQueryMethods.Count() != 1)
            {
                throw new InvalidOperationException("Unexpected Exception. Could not find the ExecuteQuery method via Reflection.");
            }

            var genericMethod = executeQueryMethods.First().MakeGenericMethod(queryReturnType);

            try
            {
                return genericMethod.Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private Type GetQueryReturnType(Type queryType)
        {
            var queryInterface = queryType.GetInterfaces().Single(i => i.IsGenericType && i.Name.StartsWith("IQuery"));
            return queryInterface.GenericTypeArguments[0];
        }

        private Type GetQueryType(string queryName)
        {
            List<Type> assemblyTypes = new List<Type>();

            assemblyTypes.AddRange(typeof(BaseQuery).Assembly.GetTypes());

            return assemblyTypes.Single(t => t.IsClass && t.Name.ToLower() == $"{queryName}Query".ToLower());
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called via reflection")]
        private TResult ExecuteQuery<TResult>(IQuery query)
        {
            Resolver.Container.RegisterInstance<HttpContextWrapper>((HttpContextWrapper)Request.Properties["MS_HttpContext"]);

            var queryHandlerFactory = Resolver.Container.Resolve<IQueryHandlerFactory>();
            var queryFactory = Resolver.Container.Resolve<IQueryFactory>();
            var result = queryHandlerFactory.Execute<TResult>(queryFactory.Create(query));
            return result;
        }
    }
}
