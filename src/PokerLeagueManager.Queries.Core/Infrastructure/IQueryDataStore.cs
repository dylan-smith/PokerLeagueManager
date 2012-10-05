using PokerLeagueManager.Common.DTO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryDataStore
    {
        void Insert<T>(T dto) where T : IDataTransferObject;
        IEnumerable<T> GetData<T>() where T : IDataTransferObject;
    }
}
