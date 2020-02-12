using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO.Lookups
{
    [DataContract]
    public class GamePlayersLookupDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        [Description("Guid that uniquely identifies the player")]
        public Guid PlayerId { get; set; }
    }
}
