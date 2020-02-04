using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Common.Tests
{
    public class FakeQueryDataStore : IQueryDataStore
    {
        private readonly Dictionary<Type, List<IDataTransferObject>> _dataStore = new Dictionary<Type, List<IDataTransferObject>>();

        public void Insert<T>(T dto)
            where T : class, IDataTransferObject
        {
            if (!_dataStore.ContainsKey(typeof(T)))
            {
                _dataStore.Add(typeof(T), new List<IDataTransferObject>());
            }

            _dataStore[typeof(T)].Add(dto);
        }

        public IQueryable<T> GetData<T>()
            where T : class, IDataTransferObject
        {
            if (!_dataStore.ContainsKey(typeof(T)))
            {
                return new List<T>().AsQueryable<T>();
            }

            return _dataStore[typeof(T)].Cast<T>().AsQueryable<T>();
        }

        public void Delete<T>(Guid dtoId)
            where T : class, IDataTransferObject
        {
            var dtoToDelete = _dataStore[typeof(T)].Single(d => d.DtoId == dtoId);
            _dataStore[typeof(T)].Remove(dtoToDelete);
        }

        public void Delete<T>(T dto)
            where T : class, IDataTransferObject
        {
            _dataStore[typeof(T)].Remove(dto);
        }

        public void Update<T>(T dto)
            where T : class, IDataTransferObject
        {
            var cur = _dataStore[typeof(T)].Single(x => x.DtoId == dto.DtoId);

            _dataStore[typeof(T)].Remove(cur);
            _dataStore[typeof(T)].Add(dto);
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
