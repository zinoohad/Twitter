using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DataBaseConnections
{
    abstract class SQLMethods
    {
        #region Params

        protected string server;        // Host server address

        protected string database;      // Database name
            
        protected string userName;      // Database user name

        protected string password;      // Database password

        protected string port;          // Database port

        protected DbConnection connection = null;   // Contatins the database connection type

        protected string _ConnectionString;         // Contatins the DB string connection

        public string ConnectionErrorMessage = "";  

        public string InsertMessage = "";

        public string SelectErrorMessage = "";

        public string ExecuteMessage = "";

        public ConnectionState State { get { return connection.State; } }

        public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }

        #endregion

        #region Functions

        public DbConnection getConnectionObject() { return connection; }

        public bool isOpen() { return connection.State == ConnectionState.Open; }

        public bool isClosed() { return connection.State == ConnectionState.Closed; }

        public void CloseConnection()
        {
            if (isOpen())
                connection.Close();
        }

        public void OpenConnection()
        {
            if (isClosed())
                connection.Open();
        }

        #endregion

        #region Abstract / Virtual Functions

        abstract public DataTable Select(string sqlQuery);

        abstract public long Insert(string sqlQuery,bool returnInsertedID = false, string columnName = "ID");

        abstract public long Update(string sqlQuery, bool returnUpdatedID = false, string columnName = "ID");

        virtual public long Delete(string sqlQuery, bool returnDeletedID = false, string columnName = "ID") { return ExecuteNonQuery(sqlQuery); }

        abstract public long ExecuteNonQuery(string sqlQuery);

        /// <summary>
        /// This function return id from the insert / update record.
        /// Must to set an "OUTPUT" statement to the query.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        abstract public long ExecuteScalar(string sqlQuery);

        #endregion
        
        

    }
}
