using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.Infrastructure
{
    public class FakeQueryDataStore : IQueryDataStore
    {
        private Dictionary<Type, List<IDataTransferObject>> dataStore = new Dictionary<Type, List<IDataTransferObject>>();

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
    }
}
