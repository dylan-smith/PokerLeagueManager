using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayersQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayersQuery, IEnumerable<GetPlayersDto>>
    {
        public IEnumerable<GetPlayersDto> Execute(GetPlayersQuery query)
        {
            return Repository.GetData<GetPlayersDto>().OrderByDescending(x => x.GamesPlayed).ToList();
        }
    }
}