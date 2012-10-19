using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGameCountByDateDTO : IDataTransferObject
    {
        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public int GameYear { get; set; }

        [DataMember]
        public int GameMonth { get; set; }

        [DataMember]
        public int GameDay { get; set; }
    }
}
