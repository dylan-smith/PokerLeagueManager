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
        private SqlTransaction _transaction;
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

            try
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand))
                {
                    OpenConnection();
                    myAdapter.Fill(result);
                    CloseConnection();
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

            OpenConnection();
            int result = myCommand.ExecuteNonQuery();
            CloseConnection();

            return result;
        }

        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, new object[0]);
        }

        public object ExecuteScalar(string sql, params object[] sqlArgs)
        {
            SqlCommand myCommand = this.PrepareCommand(sql, sqlArgs);

            OpenConnection();
            object result = myCommand.ExecuteScalar();
            CloseConnection();

            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ExecuteInTransaction(Action work)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            try
            {
                work();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _connection.Close();

                _transaction.Dispose();
                _transaction = null;
            }
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
                        this._transaction.Dispose();
                    }
                }
            }

            this._disposedValue = true;
        }

        private void CloseConnection()
        {
            if (_transaction == null)
            {
                _connection.Close();
            }
        }

        private void OpenConnection()
        {
            if (_transaction == null)
            {
                _connection.Open();
            }
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

                myCommand.Transaction = _transaction;

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
