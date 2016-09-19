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

        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return base.Channel.ExecuteQueryDto(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return base.Channel.ExecuteQueryInt(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return base.Channel.ExecuteQueryList(query);
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "This Exception should never happen, so I'm ok with leaving it as-is")]
        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            if (typeof(IDataTransferObject).IsAssignableFrom(typeof(TResult)))
            {
                return (TResult)ExecuteQueryDto(query);
            }

            if (typeof(IEnumerable<IDataTransferObject>).IsAssignableFrom(typeof(TResult)))
            {
                var resultType = typeof(TResult);
                var dtoType = resultType.GenericTypeArguments[0];
                var castMethod = typeof(IEnumerable).GetExtensionMethod("Cast");
                var genericCastMethod = castMethod.MakeGenericMethod(dtoType);

                var dtoList = ExecuteQueryList(query);
                return (TResult)genericCastMethod.Invoke(dtoList, new object[] { dtoList });
            }

            if (typeof(int) == typeof(TResult))
            {
                return (TResult)(object)ExecuteQueryInt(query);
            }

            throw new Exception("Unexpected return type");
        }
    }
}
