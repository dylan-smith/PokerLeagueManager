using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryDataStore : IQueryDataStore
    {
        private IDatabaseLayer _databaseLayer;
        private IDTOFactory _dtoFactory;

        public QueryDataStore(IDatabaseLayer databaseLayer, IDTOFactory dtoFactory)
        {
            _databaseLayer = databaseLayer;
            _dtoFactory = dtoFactory;
        }

        public void Insert<T>(T dto) where T : IDataTransferObject
        {
            var tableName = dto.GetType().Name;
            var fieldList = string.Empty;
            var valueList = string.Empty;

            foreach (var prop in dto.GetType().GetProperties())
            {
                fieldList += string.Format("{0}, ", prop.Name);
                valueList += string.Format("{0}, ", prop.GetValue(dto));
            }

            fieldList = fieldList.Substring(0, fieldList.Length - 2);
            valueList = valueList.Substring(0, valueList.Length - 2);

            // TODO: Need to use parameterized SQL otherwise we're open to SQL Injection attacks
            var sql = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, fieldList, valueList);

            _databaseLayer.ExecuteNonQuery(sql);
        }

        public IEnumerable<T> GetData<T>() where T : IDataTransferObject
        {
            var tableName = typeof(T).Name;

            var sql = string.Format("SELECT * FROM {0}", tableName);

            var dataTable = _databaseLayer.GetDataTable(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                yield return _dtoFactory.Create<T>(row);
            }
        }
    }
}
