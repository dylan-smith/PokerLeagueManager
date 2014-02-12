using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.DTO.Infrastructure
{
    [DataContract]
    public class BaseDataTransferObject : IDataTransferObject
    {
        public BaseDataTransferObject()
        {
            DtoId = Guid.NewGuid();
        }

        [DataMember]
        public Guid DtoId { get; set; }
    }
}
