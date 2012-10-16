using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Utilities
{
    public class SQLServerDatabaseLayer : IDatabaseLayer, IDisposable
    {
        private SqlConnection _connection;
        private bool _disposedValue;
        private string _connectionString;

        public SQLServerDatabaseLayer()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        ~SQLServerDatabaseLayer()
        {
            this.Dispose(false);
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
                _connection = new SqlConnection(_connectionString);
            }
        }

        public DataTable GetDataTable(string sqlString)
        {
            return this.GetDataTable(sqlString, new object[0]);
        }

        public DataTable GetDataTable(string sqlString, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sqlString, sqlArgs);

            if (myCommand == null)
            {
                return null;
            }

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            DataTable result = new DataTable();
            _connection.Open();
            myAdapter.Fill(result);
            _connection.Close();
            return result;
        }

        public int ExecuteNonQuery(string sqlString)
        {
            return this.ExecuteNonQuery(sqlString, new object[0]);
        }

        public int ExecuteNonQuery(string sqlString, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sqlString, sqlArgs);

            if (myCommand == null)
            {
                return 0;
            }

            _connection.Open();
            int result = myCommand.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        public object ExecuteScalar(string sqlString)
        {
            return this.ExecuteScalar(sqlString, new object[0]);
        }

        public object ExecuteScalar(string sqlString, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sqlString, sqlArgs);

            if (myCommand == null)
            {
                return 0;
            }

            _connection.Open();
            object result = myCommand.ExecuteScalar();
            _connection.Close();
            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    if (this._connection != null)
                    {
                        if (this._connection.State == ConnectionState.Open)
                        {
                            this._connection.Close();
                        }

                        this._connection.Dispose();
                    }
                }
            }

            this._disposedValue = true;
        }

        private SqlCommand PrepareCommand(string sqlString, params object[] sqlArgs)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                throw new ArgumentException("The SQL statement was blank. A valid SQL Statement must be provided.", "sqlString");
            }

            SqlCommand myCommand;

            myCommand = new SqlCommand(sqlString, this._connection);
            myCommand.CommandTimeout = 0;
            myCommand.CommandType = CommandType.Text;

            for (int i = 0; i < sqlArgs.Length; i += 2)
            {
                myCommand.Parameters.AddWithValue((string)sqlArgs[i], sqlArgs[i + 1]);
            }

            return myCommand;
        }
    }
}
