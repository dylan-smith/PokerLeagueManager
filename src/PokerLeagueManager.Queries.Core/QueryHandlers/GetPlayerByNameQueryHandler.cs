using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetPlayerByNameQueryHandler : BaseQueryHandler, IHandlesQuery<GetPlayerByNameQuery>
    {
        public object Execute(GetPlayerByNameQuery query)
        {
            return Repository.GetData<GetPlayerByNameDto>().FirstOrDefault(p => p.PlayerName.ToUpper().Trim() == query.PlayerName.ToUpper().Trim());
        }
    }
}