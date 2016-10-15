using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGamePlayersDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        [Description("What place the player got in this game")]
        public int Placing { get; set; }

        [DataMember]
        [Description("How much in dollars the player won in this game")]
        public int Winnings { get; set; }

        [DataMember]
        [Description("The total amount the player paid in this game (buyin + rebuys)")]
        public int PayIn { get; set; }
    }
}
