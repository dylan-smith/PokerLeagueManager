using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerByNameQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerByNameQuery, GetPlayerByNameDto>
    {
        public GetPlayerByNameDto Execute(GetPlayerByNameQuery query)
        {
            return Repository.GetData<GetPlayerByNameDto>().FirstOrDefault(p => p.PlayerName.ToUpper().Trim() == query.PlayerName.ToUpper().Trim());
        }
    }
}