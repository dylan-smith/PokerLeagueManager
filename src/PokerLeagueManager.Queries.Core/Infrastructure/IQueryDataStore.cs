using System;
using System.Linq;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryDataStore
    {
        void Insert<T>(T dto)
            where T : class, IDataTransferObject;

        IQueryable<T> GetData<T>()
            where T : class, IDataTransferObject;

        void Delete<T>(Guid dtoId)
            where T : class, IDataTransferObject;

        void Delete<T>(T dto)
            where T : class, IDataTransferObject;

        void Update<T>(T dto)
            where T : class, IDataTransferObject;

        int SaveChanges();
    }
}
