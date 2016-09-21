using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetGamesWithPlayerQueryHandler : BaseQueryHandler, IHandlesQuery<GetGamesWithPlayerQuery, IEnumerable<GetGamesWithPlayerDto>>
    {
        public IEnumerable<GetGamesWithPlayerDto> Execute(GetGamesWithPlayerQuery query)
        {
            return Repository.GetData<GetGamesWithPlayerDto>().Where(g => g.PlayerName.ToUpper().Trim() == query.PlayerName.ToUpper().Trim()).ToList();
        }
    }
}