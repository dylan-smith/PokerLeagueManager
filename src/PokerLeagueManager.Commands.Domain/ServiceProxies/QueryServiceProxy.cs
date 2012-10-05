using PokerLeagueManager.Common.DTO;
using System;
using System.ServiceModel;

namespace PokerLeagueManager.Commands.Domain.ServiceProxies
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public int GetGameCountByDate(DateTime gameDate)
        {
            return base.Channel.GetGameCountByDate(gameDate);
        }
    }
}
