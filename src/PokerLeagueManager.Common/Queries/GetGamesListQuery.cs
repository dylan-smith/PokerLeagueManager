using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets a list of games")]
    [Description("Used for showing a list of games, includes some basic stats about the game (e.g. Winner, Total Pot, etc")]
    public class GetGamesListQuery : BaseQuery, IQuery<IEnumerable<GetGamesListDto>>
    {
        [DataMember]
        [Description("Used in combination with Take to do paging of the results. Skip = 20, Take = 10 will retrieve items 21-30")]
        public int Skip { get; set; }

        [DataMember]
        [Description("Used in combination with Skip to do paging of the results. Skip = 20, Take = 10 will retrieve items 21-30")]
        public int Take { get; set; }
    }
}
