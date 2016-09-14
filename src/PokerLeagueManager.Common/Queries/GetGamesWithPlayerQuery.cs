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
        public string PlayerName { get; set; }
    }
}
