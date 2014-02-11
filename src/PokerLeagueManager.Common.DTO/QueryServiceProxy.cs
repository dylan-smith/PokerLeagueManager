using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PokerLeagueManager.Common.DTO
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public int GetGameCountByDate(DateTime gameDate)
        {
            return base.Channel.GetGameCountByDate(gameDate);
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            return base.Channel.GetGamesList();
        }
    }
}
