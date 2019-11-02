using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerCountByNameQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerCountByNameQuery, int>
    {
        public int Execute(GetPlayerCountByNameQuery query)
        {
            return Repository.GetData<GetPlayerCountByNameDto>().Count(x => x.PlayerName.ToUpper() == query.PlayerName.ToUpper());
        }
    }
}