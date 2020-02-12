using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO.Lookups
{
    [DataContract]
    public class GameLookupDto : BaseDataTransferObject
    {
        [DataMember]
        [Description("Guid that uniquely identifies the game")]
        public Guid GameId { get; set; }

        [DataMember]
        [Description("Date that the game took place")]
        public DateTime GameDate { get; set; }

        [DataMember]
        [Description("Indicates whether all the data from the game has been input")]
        public bool Completed { get; set; }
    }
}
