using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EFSpike.Domain;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetGameResultsDto : BaseDataTransferObject
    {
        public GetGameResultsDto()
        {
            Players = new List<PlayerDto>();
        }

        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public DateTime GameDate { get; set; }

        [DataMember]
        public ICollection<PlayerDto> Players { get; private set; }
    }
}
