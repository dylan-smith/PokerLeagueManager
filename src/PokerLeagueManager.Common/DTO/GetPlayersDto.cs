using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetPlayersDto : BaseDataTransferObject
    {
        [DataMember]
        public Guid PlayerId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int GamesPlayed { get; set; }
    }
}
