using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets a list of GameIds that a specific player participated in")]
    [Description("Used when performing batch operations for a player (e.g. Rename)")]
    public class GetGamesWithPlayerQuery : BaseQuery, IQuery<IEnumerable<GetGamesWithPlayerDto>>
    {
        public GetGamesWithPlayerQuery()
        {
        }

        public GetGamesWithPlayerQuery(string playerName)
        {
            PlayerName = playerName;
        }

        [DataMember]
        [Description("The name of the player to search for")]
        public string PlayerName { get; set; }
    }
}
