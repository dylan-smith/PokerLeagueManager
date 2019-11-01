using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGamePlayersDto : BaseDataTransferObject
    {
        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public Guid PlayerId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int PayIn { get; set; }

        [DataMember]
        public int Winnings { get; set; }

        [DataMember]
        public int Placing { get; set; }
    }
}
