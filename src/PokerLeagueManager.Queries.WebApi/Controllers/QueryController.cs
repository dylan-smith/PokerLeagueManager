using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.UI.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
