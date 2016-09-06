using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IDataTransferObject
    {
        Guid DtoId { get; set; }
    }
}
