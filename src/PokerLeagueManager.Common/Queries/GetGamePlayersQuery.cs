using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    public class GetGamePlayersQuery : BaseQuery, IQuery<IEnumerable<GetGamePlayersDto>>
    {
        public GetGamePlayersQuery()
        {
        }

        public GetGamePlayersQuery(Guid gameId)
        {
            GameId = gameId;
        }

        [DataMember]
        public Guid GameId { get; set; }
    }
}
