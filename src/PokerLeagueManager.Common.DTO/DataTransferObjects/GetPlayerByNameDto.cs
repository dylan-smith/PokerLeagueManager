using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.DTO.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetPlayerByNameDto : BaseDataTransferObject
    {
        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int GameCount { get; set; }
    }
}
