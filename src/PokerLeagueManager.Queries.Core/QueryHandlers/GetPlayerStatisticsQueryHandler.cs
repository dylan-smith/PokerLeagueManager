using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerStatisticsQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerStatisticsQuery, IEnumerable<GetPlayerStatisticsDto>>
    {
        public IEnumerable<GetPlayerStatisticsDto> Execute(GetPlayerStatisticsQuery query)
        {
            return Repository.GetData<GetPlayerStatisticsDto>().ToList();
        }
    }
}