using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.DTO.Infrastructure
{
    public class BaseDataTransferObject : IDataTransferObject
    {
        public Guid DtoId { get; set; }
    }
}
