using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the list of players and results in a specific game.")]
    [Description("Gets the list of players in a specific game, and the results for that player (e.g. Placing, Winnings, PayIn, etc)")]
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
        [Description("The GUID of the game to retrieve data for")]
        public Guid GameId { get; set; }
    }
}
