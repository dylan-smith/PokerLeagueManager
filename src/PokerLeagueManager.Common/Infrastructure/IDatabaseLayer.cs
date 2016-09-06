using System;
using System.Data;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IDatabaseLayer
    {
        string ConnectionString { get; set; }

        int ExecuteNonQuery(string sql);

        int ExecuteNonQuery(string sql, params object[] sqlArgs);

        object ExecuteScalar(string sql);

        object ExecuteScalar(string sql, params object[] sqlArgs);

        DataTable GetDataTable(string sql);

        DataTable GetDataTable(string sql, params object[] sqlArgs);

        void ExecuteInTransaction(Action work);
    }
}
