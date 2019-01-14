using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    [Summary("Gets the number of games on a given date")]
    [Description("Used to test for duplicate games on the same date.")]
    public class GetGameCountByDateQuery : BaseQuery, IQuery<int>
    {
        public GetGameCountByDateQuery()
        {
        }

        public GetGameCountByDateQuery(DateTime gameDate)
        {
            GameDate = gameDate;
        }

        [DataMember]
        [Description("The date that you want to check for games on")]
        public DateTime GameDate { get; set; }
    }
}
