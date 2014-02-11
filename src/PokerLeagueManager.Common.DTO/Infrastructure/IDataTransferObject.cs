using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.DTO.Infrastructure
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Used by the plumbing to detect DTO types.  Could possibly use an attribute, but interface is more convenient and will probably include members later")]
    public interface IDataTransferObject
    {
        Guid DtoId { get; set; }
    }
}
