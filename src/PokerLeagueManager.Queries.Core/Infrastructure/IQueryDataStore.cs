using System.Collections.Generic;
using PokerLeagueManager.Common.DTO.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryDataStore
    {
        void Insert<T>(T dto) where T : IDataTransferObject;

        IEnumerable<T> GetData<T>() where T : IDataTransferObject;
    }
}
