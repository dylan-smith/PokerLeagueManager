using System;
using System.ServiceModel;

namespace PokerLeagueManager.Common.DTO
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public int GetGameCountByDate(DateTime gameDate)
        {
            return base.Channel.GetGameCountByDate(gameDate);
        }
    }
}
