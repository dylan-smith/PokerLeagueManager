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
        public QueryController()
        {
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            using (var svc = new QueryServiceProxy())
            {
                return svc.Execute<IEnumerable<GetGamesListDto>>(new GetGamesListQuery());
            }
        }

        public IEnumerable<GetGamePlayersDto> GetGamePlayers(Guid gameId)
        {
            using (var svc = new QueryServiceProxy())
            {
                return svc.Execute<IEnumerable<GetGamePlayersDto>>(new GetGamePlayersQuery(gameId));
            }
        }
    }
}
