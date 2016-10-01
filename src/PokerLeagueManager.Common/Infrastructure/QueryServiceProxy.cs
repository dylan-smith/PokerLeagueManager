using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public QueryServiceProxy()
        {
            var queryUrl = ConfigurationManager.AppSettings["QueryServiceUrl"];
            base.Endpoint.Address = new EndpointAddress(queryUrl);
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            return (TResult)query;

            // TODO: Actually call the REST API and convert between JSON
        }
    }
}
