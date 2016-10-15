using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGamesWithPlayerDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }
    }
}
