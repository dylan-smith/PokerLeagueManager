using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the number of games on a given date")]
    public class GetGamesListQuery : BaseQuery, IQuery<IEnumerable<GetGamesListDto>>
    {
        public GetGamesListQuery()
        {
        }

        public GetGamesListQuery(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        [DataMember]
        [Description("The number of games that you want to skip - i.e. that you have already retrieved")]
        public int Skip { get; set; }

        [DataMember]
        [Description("The maximum number of games you want to be returned - 0 will return all")]
        public int Take { get; set; }
    }
}
