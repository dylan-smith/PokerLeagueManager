using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryDataStore : IQueryDataStore
    {
        private IDatabaseLayer _databaseLayer;

        // TODO: Definately write some unit tests for this class
        public QueryDataStore(IDatabaseLayer databaseLayer)
        {
            _databaseLayer = databaseLayer;
        }

        public void Insert<T>(T dto) where T : IDataTransferObject
        {
            // TODO: reflect over T and build an INSERT statement
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetData<T>()
        {
            // TODO: use reflection to get the tablename and do a SELECT *
            // TODO: then use a DTOFactory to turn the results into the right DTO
            throw new NotImplementedException();
        }
    }
}
