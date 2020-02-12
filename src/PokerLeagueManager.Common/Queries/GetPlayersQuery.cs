using System.Collections.Generic;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the players in a game")]
    public class GetPlayersQuery : BaseQuery, IQuery<IEnumerable<GetPlayersDto>>
    {
        public GetPlayersQuery()
        {
        }
    }
}
