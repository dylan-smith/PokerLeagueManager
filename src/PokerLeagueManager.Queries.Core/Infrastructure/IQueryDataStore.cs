using System.Collections.Generic;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryDataStore
    {
        IDatabaseLayer DatabaseLayer { get; set; }

        void Insert<T>(T dto) where T : IDataTransferObject;

        IEnumerable<T> GetData<T>() where T : IDataTransferObject;
    }
}
