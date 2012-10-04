using PokerLeagueManager.Common.DTO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
