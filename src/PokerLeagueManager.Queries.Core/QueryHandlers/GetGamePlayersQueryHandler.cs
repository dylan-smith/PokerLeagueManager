using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetGamePlayersQueryHandler : BaseQueryHandler, IHandlesQuery<GetGamePlayersQuery, IEnumerable<GetGamePlayersDto>>
    {
        public IEnumerable<GetGamePlayersDto> Execute(GetGamePlayersQuery query)
        {
            return Repository.GetData<GetGamePlayersDto>().Where(x => x.GameId == query.GameId).ToList();
        }
    }
}