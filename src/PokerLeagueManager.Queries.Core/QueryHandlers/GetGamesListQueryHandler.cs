using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetGamesListQueryHandler : BaseQueryHandler, IHandlesQuery<GetGamesListQuery, IEnumerable<GetGamesListDto>>
    {
        public IEnumerable<GetGamesListDto> Execute(GetGamesListQuery query)
        {
            return Repository.GetData<GetGamesListDto>().ToList();
        }
    }
}