﻿using System;
using System.Data;
using System.Linq;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class DtoFactory : IDtoFactory
    {
        public T Create<T>(DataRow row)
            where T : IDataTransferObject
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row), "DataRow cannot be null");
            }

            var result = System.Activator.CreateInstance<T>();

            foreach (DataColumn col in row.Table.Columns)
            {
                var dataValue = row[col];
                var propName = col.ColumnName;

                if (typeof(T).GetProperties().Any(x => x.Name == propName))
                {
                    var prop = typeof(T).GetProperties().First(x => x.Name == propName);
                    prop.SetValue(result, dataValue);
                }
            }

            return result;
        }
    }
}
