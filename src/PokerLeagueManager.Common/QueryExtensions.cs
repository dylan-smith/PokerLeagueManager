using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Common
{
    public static class QueryExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Needed in order to infer generic type")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Should never happen")]
        public static TResult Execute<TResult>(this IQuery<TResult> query, IQueryService queryService)
        {
            if (typeof(IDataTransferObject).IsAssignableFrom(typeof(TResult)))
            {
                return (TResult)queryService.ExecuteQueryDto(query);
            }

            if (typeof(IEnumerable<IDataTransferObject>).IsAssignableFrom(typeof(TResult)))
            {
                var resultType = typeof(TResult);
                var dtoType = resultType.GenericTypeArguments[0];
                var castMethod = typeof(IEnumerable).GetExtensionMethod("Cast");
                var genericCastMethod = castMethod.MakeGenericMethod(dtoType);

                var dtoList = queryService.ExecuteQueryList(query);
                return (TResult)genericCastMethod.Invoke(dtoList, new object[] { dtoList });
            }

            if (typeof(int) == typeof(TResult))
            {
                return (TResult)(object)queryService.ExecuteQueryInt(query);
            }

            throw new Exception("Unexpected return type");
        }
    }
}
