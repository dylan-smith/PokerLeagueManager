using System.ComponentModel;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.DTO
{
    [DataContract]
    public class GetPlayerByNameDto : BaseDataTransferObject
    {
        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        [Description("The number of games this player has played in")]
        public int GameCount { get; set; }
    }
}
