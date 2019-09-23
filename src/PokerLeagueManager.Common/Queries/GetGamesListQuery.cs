using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the number of games on a given date")]
    [Description("Used to test for duplicate games on the same date.")]
    public class GetGamesListQuery : BaseQuery, IQuery<IEnumerable<GetGamesListDto>>
    {
    }
}
