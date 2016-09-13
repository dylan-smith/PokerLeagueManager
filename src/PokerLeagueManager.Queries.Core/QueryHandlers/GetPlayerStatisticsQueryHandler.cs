using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerStatisticsQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerStatisticsQuery>
    {
        public object Execute(GetPlayerStatisticsQuery query)
        {
            return Repository.GetData<GetPlayerStatisticsDto>();
        }
    }
}