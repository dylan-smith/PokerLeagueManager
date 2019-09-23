using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGameCountByDateDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        public int GameYear { get; set; }

        [DataMember]
        public int GameMonth { get; set; }

        [DataMember]
        public int GameDay { get; set; }
    }
}
