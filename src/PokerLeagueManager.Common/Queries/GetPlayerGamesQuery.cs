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
    [Summary("Gets a list of all games a player has played in")]
    [Description("Also retrieves info on how that player did in each game.")]
    public class GetPlayerGamesQuery : BaseQuery, IQuery<IEnumerable<GetPlayerGamesDto>>
    {
        public GetPlayerGamesQuery()
        {
        }

        public GetPlayerGamesQuery(string playerName)
        {
            PlayerName = playerName;
        }

        [DataMember]
        [Description("The player name to search for")]
        public string PlayerName { get; set; }
    }
}
