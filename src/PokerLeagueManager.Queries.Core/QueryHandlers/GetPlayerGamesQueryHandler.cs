using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerGamesQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerGamesQuery, IEnumerable<GetPlayerGamesDto>>
    {
        public IEnumerable<GetPlayerGamesDto> Execute(GetPlayerGamesQuery query)
        {
            return Repository.GetData<GetPlayerGamesDto>().Where(g => g.PlayerName.ToUpper().Trim() == query.PlayerName.ToUpper().Trim());
        }
    }
}