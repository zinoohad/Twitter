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
        protected string server;
        protected string database;
        protected string userName;
        protected string password;
        protected string port;
        protected DbConnection connection = null;
        protected string _ConnectionString;
        public string ConnectionErrorMessage = "";
        public string InsertMessage = "";
        public string SelectErrorMessage = "";
        public string ExecuteMessage = "";
        public ConnectionState State { get { return connection.State; } }
        public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        abstract public DataTable Select(string sqlQuery);
        abstract public int Insert(string sqlQuery);
        abstract public int Update(string sqlQuery);
        abstract public int ExecuteNonQuery(string sqlQuery);      
        public DbConnection getConnectionObject() { return connection; }
        public bool isOpen() { return connection.State == ConnectionState.Open; }
        public bool isClosed() { return connection.State == ConnectionState.Closed; }
        public bool CloseConnection()
        {
            try
            {
                if (isOpen())
                    connection.Close();
            }
            catch (Exception e)
            {
                ConnectionErrorMessage = e.Message;
                return false;
            }
            return true;
        }
        public bool OpenConnection()
        {
            try
            {
                if (isClosed())
                    connection.Open();
            }
            catch (Exception e)
            {
                ConnectionErrorMessage = e.Message;
                return false;
            }
            return true;
        }

    }
}
