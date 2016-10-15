using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets a list of players and their stats")]
    [Description("These are the players lifetime stats across all games")]
    public class GetPlayerStatisticsQuery : BaseQuery, IQuery<IEnumerable<GetPlayerStatisticsDto>>
    {
    }
}
