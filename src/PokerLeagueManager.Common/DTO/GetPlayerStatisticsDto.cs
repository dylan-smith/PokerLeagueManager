using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetPlayerStatisticsDto : BaseDataTransferObject
    {
        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        [Description("The total number of games this player has played in")]
        public int GamesPlayed { get; set; }

        [DataMember]
        [Description("The total amount of money won across all games")]
        public int Winnings { get; set; }

        [DataMember]
        [Description("The total amount this player has spent (buyins + rebuys) across all games")]
        public int PayIn { get; set; }

        [DataMember]
        [Description("The total amount of profit (Winnings - PayIn) this player has made across all games")]
        public int Profit { get; set; }

        [DataMember]
        [Description("The average amount of profit this player makes at each game (Profit / Games)")]
        public double ProfitPerGame { get; set; }
    }
}
