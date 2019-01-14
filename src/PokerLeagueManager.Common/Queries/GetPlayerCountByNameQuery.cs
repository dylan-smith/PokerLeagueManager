using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the number of players with a given name")]
    [Description("Used to test for duplicate players with the same name.")]
    public class GetPlayerCountByNameQuery : BaseQuery, IQuery<int>
    {
        public GetPlayerCountByNameQuery()
        {
        }

        public GetPlayerCountByNameQuery(string playerName)
        {
            PlayerName = playerName;
        }

        [DataMember]
        [Description("The player name that you want to check for duplicates")]
        public string PlayerName { get; set; }
    }
}
