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
    [Summary("Gets a count of games a specific player has played in")]
    [Description("Used to test for the existence of a player name (e.g. when renaming a player)")]
    public class GetPlayerByNameQuery : BaseQuery, IQuery<GetPlayerByNameDto>
    {
        public GetPlayerByNameQuery()
        {
        }

        public GetPlayerByNameQuery(string playerName)
        {
            PlayerName = playerName;
        }

        [DataMember]
        [Description("The player name to search for")]
        public string PlayerName { get; set; }
    }
}
