using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Common.Tests
{
    public class FakeQueryDataStore : IQueryDataStore
    {
        private Dictionary<Type, List<IDataTransferObject>> dataStore = new Dictionary<Type, List<IDataTransferObject>>();

        public IDatabaseLayer DatabaseLayer { get; set; }

        public void Insert<T>(T dto) where T : IDataTransferObject
        {
            if (!dataStore.ContainsKey(typeof(T)))
            {
                dataStore.Add(typeof(T), new List<IDataTransferObject>());
            }

            dataStore[typeof(T)].Add(dto);
        }

        public IEnumerable<T> GetData<T>() where T : IDataTransferObject
        {
            if (!dataStore.ContainsKey(typeof(T)))
            {
                return new List<T>();
            }

            return dataStore[typeof(T)].Cast<T>();
        }

        public T GetData<T>(Func<T, bool> filter) where T : IDataTransferObject
        {
            return GetData<T>().FirstOrDefault(filter);
        }

        public void Update<T>(T dto) where T : IDataTransferObject
        {
            var old = dataStore[typeof(T)].First(x => x.DtoId == dto.DtoId);

            dataStore[typeof(T)].Remove(old);
            dataStore[typeof(T)].Add(dto);
        }
    }
}
