using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryDataStore : IQueryDataStore
    {
        private IDtoFactory _dtoFactory;

        public QueryDataStore(IDtoFactory dtoFactory)
        {
            _dtoFactory = dtoFactory;
        }

        public IDatabaseLayer DatabaseLayer { get; set; }

        public void Insert<T>(T dto) where T : IDataTransferObject
        {
            var tableName = GetTableName(typeof(T));
            var fieldList = BuildFieldList(dto.GetType().GetProperties());
            var valueList = BuildValueList(dto.GetType().GetProperties());
            var valueArray = BuildValueArray(dto.GetType().GetProperties(), dto);

            var sql = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, fieldList, valueList);

            DatabaseLayer.ExecuteNonQuery(sql, valueArray);
        }

        public IEnumerable<T> GetData<T>() where T : IDataTransferObject
        {
            var tableName = GetTableName(typeof(T));

            var sql = string.Format("SELECT * FROM {0}", tableName);

            var dataTable = DatabaseLayer.GetDataTable(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                yield return _dtoFactory.Create<T>(row);
            }
        }

        private string BuildFieldList(PropertyInfo[] properties)
        {
            var result = new StringBuilder();

            foreach (var prop in properties)
            {
                result.AppendFormat("{0}, ", prop.Name);
            }

            result.Remove(result.Length - 2, 2);

            return result.ToString();
        }

        private string BuildValueList(PropertyInfo[] properties)
        {
            var result = new StringBuilder();

            foreach (var prop in properties)
            {
                result.AppendFormat("@{0}, ", prop.Name);
            }

            result.Remove(result.Length - 2, 2);

            return result.ToString();
        }

        private object[] BuildValueArray<T>(PropertyInfo[] properties, T dto)
        {
            object[] result = new object[properties.Length * 2];

            for (int x = 0; x < result.Length; x += 2)
            {
                result[x] = string.Format("@{0}", properties[x / 2].Name);
                result[x + 1] = properties[x / 2].GetValue(dto);
            }

            return result;
        }

        private string GetTableName(Type dtoType)
        {
            var tableName = dtoType.Name;

            if (tableName.Substring(tableName.Length - 3).ToUpper() == "DTO")
            {
                tableName = tableName.Substring(0, tableName.Length - 3);
            }

            return tableName;
        }
    }
}
