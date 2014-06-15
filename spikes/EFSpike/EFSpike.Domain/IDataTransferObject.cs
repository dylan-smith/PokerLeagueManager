using System;

namespace EFSpike.Domain
{
    public interface IDataTransferObject
    {
        Guid DtoId { get; set; }
    }
}
