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
    public class SqlServerDatabaseLayer : IDatabaseLayer, IDisposable
    {
        private SqlConnection _connection;
        private bool _disposedValue;
        private string _connectionString;

        public SqlServerDatabaseLayer()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        ~SqlServerDatabaseLayer()
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

        public DataTable GetDataTable(string sql)
        {
            return this.GetDataTable(sql, new object[0]);
        }

        public DataTable GetDataTable(string sql, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sql, sqlArgs);

            DataTable result = new DataTable();

            // this looks like a cyclomatic complexity nightmare, but I don't know a cleaner way to do it while respecting IDisposable
            try
            {
                SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);

                try
                {
                    _connection.Open();
                    myAdapter.Fill(result);
                    _connection.Close();
                }
                finally
                {
                    myAdapter.Dispose();
                }

                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, new object[0]);
        }

        public int ExecuteNonQuery(string sql, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sql, sqlArgs);

            _connection.Open();
            int result = myCommand.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, new object[0]);
        }

        public object ExecuteScalar(string sql, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sql, sqlArgs);

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "It will be the responsibility of the caller to ensure they aren't vulnerable to SQL Injection")]
        private SqlCommand PrepareCommand(string sql, params object[] sqlArgs)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentException("The SQL statement was blank. A valid SQL Statement must be provided.", "sql");
            }

            SqlCommand myCommand = new SqlCommand(sql, this._connection);

            try
            {
                myCommand.CommandTimeout = 0;
                myCommand.CommandType = CommandType.Text;

                for (int i = 0; i < sqlArgs.Length; i += 2)
                {
                    myCommand.Parameters.AddWithValue((string)sqlArgs[i], sqlArgs[i + 1]);
                }

                return myCommand;
            }
            catch
            {
                myCommand.Dispose();
                throw;
            }
        }
    }
}
