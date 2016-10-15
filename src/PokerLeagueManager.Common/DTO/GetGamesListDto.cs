using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGamesListDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        [Description("The date the game was played")]
        public DateTime GameDate { get; set; }

        [DataMember]
        [Description("The total amount in dollars that the winner received")]
        public int Winnings { get; set; }

        [DataMember]
        [Description("The player who won the game")]
        public string Winner { get; set; }
    }
}
