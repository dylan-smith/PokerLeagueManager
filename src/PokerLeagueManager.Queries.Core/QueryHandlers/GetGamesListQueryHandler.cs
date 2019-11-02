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
            var result = Repository.GetData<GetGamesListDto>().OrderBy(x => x.GameDate).Skip(query.Skip);

            if (query.Take > 0)
            {
                result = result.Take(query.Take);
            }

            return result.ToList();
        }
    }
}