using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.UI.Web.Controllers
{
    public class QueryController : ApiController
    {
        private IQueryService _queryService;

        public QueryController()
        {
            _queryService = new QueryServiceProxy();
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            return _queryService.Execute<IEnumerable<GetGamesListDto>>(new GetGamesListQuery());
        }

        public IEnumerable<GetGamePlayersDto> GetGamePlayers(Guid gameId)
        {
            return _queryService.Execute<IEnumerable<GetGamePlayersDto>>(new GetGamePlayersQuery(gameId));
        }

        public IEnumerable<GetPlayerStatisticsDto> GetPlayerStatistics()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetPlayerGamesDto> GetPlayerGames(string playerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetGamesWithPlayerDto> GetGamesWithPlayer(string playerName)
        {
            throw new NotImplementedException();
        }

        public GetPlayerByNameDto GetPlayerByName(string playerName)
        {
            throw new NotImplementedException();
        }
    }
}
