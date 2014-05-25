using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Queries.WCF.Infrastructure;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        private IQueryService _queryHandler;

        public QueryService()
        {
            _queryHandler = Resolver.Container.Resolve<IQueryService>();
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            return _queryHandler.GetGameCountByDate(gameDate);
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            return _queryHandler.GetGamesList();
        }

        public IEnumerable<GetGameResultsDto> GetGameResults(Guid gameId)
        {
            return _queryHandler.GetGameResults(gameId);
        }
    }
}
