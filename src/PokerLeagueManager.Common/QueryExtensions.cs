using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Common
{
    public static class QueryExtensions
    {
        // Supports this syntax from clients: var games = new GetGamesQuery().Execute(queryService);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "foo")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "foo")]
        public static TResult Execute<TQuery, TResult>(this IQuery<TResult> query, IQueryService queryService)
        {
            if (typeof(IDataTransferObject).IsAssignableFrom(typeof(TResult)))
            {
                return (TResult)queryService.ExecuteQueryDto(query);
            }

            if (typeof(IEnumerable<IDataTransferObject>).IsAssignableFrom(typeof(TResult)))
            {
                return (TResult)queryService.ExecuteQueryList(query);
            }

            if (typeof(int) == (typeof(TResult)))
            {
                return (TResult)queryService.ExecuteQueryInt(query);
            }

            throw new Exception("Unexpected return type");
        }
    }
}
