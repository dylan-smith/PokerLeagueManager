using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the players in a game")]
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
