using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Utilities
{
    public interface IDatabaseLayer
    {
        string ConnectionString { get; set; }

        int ExecuteNonQuery(string sqlString);

        int ExecuteNonQuery(string sqlString, params object[] sqlArgs);

        object ExecuteScalar(string sqlString);

        object ExecuteScalar(string sqlString, params object[] sqlArgs);

        DataTable GetDataTable(string sqlString);

        DataTable GetDataTable(string sqlString, params object[] sqlArgs);
    }
}
