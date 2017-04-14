using DataBaseConnections.DataBaseTypes;
using System;
using System.Data;

namespace DataBaseConnections
{
    public enum DBTypes
    {
        SQLServer,
        MySQL,
        Oracle,
        PostgreSQL
    }
    public class DBConnection
    {
        #region Private Params

        private SQLMethods _DBConnection = null;
        private string Server;
        private string Port;
        private string UserID;
        private string Password;
        private string _Database;

        #endregion

        #region Public Params

        public DBTypes DBType { get { return _DBType; } }

        #endregion

        private DBTypes _DBType;
        public string Database { get { return _Database; } }
        public string ConnectionErrorMessage { get { return _DBConnection.ConnectionErrorMessage; } }
        public string InsertMessage { get { return _DBConnection.InsertMessage; } }
        public string SelectErrorMessage { get { return _DBConnection.SelectErrorMessage; } }
        public string ExecuteMessage { get { return _DBConnection.ExecuteMessage; } }
        public ConnectionState State { get { return _DBConnection.State; } }
        public string ConnectionString { get { return _DBConnection.ConnectionString; } }        

        public DBConnection(DBTypes DBType, string Server, string Port, string UserID, string Password, string Database, bool onLocalHost = false)
        {
            this._DBType = DBType;
            this.Server = Server;
            this.Port = Port;
            this.UserID = UserID;
            this.Password = Password;
            this._Database = Database;
            switch (DBType)
            {
                case DBTypes.SQLServer:
                    _DBConnection = new SQLServerConnection(Server, Database, UserID, Password, onLocalHost);
                    break;
                case DBTypes.MySQL:
                    _DBConnection = new MySQLConnection(Server, Database, UserID, Password);
                    break;
                case DBTypes.Oracle:
                    _DBConnection = new OracleConnection(Server, Database, UserID, Password, Port);
                    break;
                case DBTypes.PostgreSQL:
                    _DBConnection = new PostgreSQLConnection(Server, Database, Port, UserID, Password);
                    break;
            }
        }

        private void CloseConnection()
        {
            _DBConnection.CloseConnection();
        }

        private void OpenConnection()
        {
            _DBConnection.OpenConnection();
        }

        public DataTable Select(string sqlQuery)
        {
            DataTable dt;
            OpenConnection();
            dt = _DBConnection.Select(sqlQuery);
            CloseConnection();
            return dt;
        }

        public long Insert(string sqlQuery, bool returnInsertedID = false, string columnName = "ID")
        {
            long recordNum;
            OpenConnection();
            recordNum = _DBConnection.Insert(sqlQuery, returnInsertedID, columnName);
            CloseConnection();
            return recordNum;
        }
        public long Update(string sqlQuery, bool returnUpdatedID = false, string columnName = "ID")
        {
            long recordNum;
            OpenConnection();
            recordNum = _DBConnection.Update(sqlQuery, returnUpdatedID, columnName);
            CloseConnection();
            return recordNum;
        }
        public long Delete(string sqlQuery, bool returnDeletedID = false, string columnName = "ID")
        {
            long recordNum;
            OpenConnection();
            recordNum = _DBConnection.Delete(sqlQuery, returnDeletedID, columnName);
            CloseConnection();
            return recordNum;
        }

        public long ExecuteNonQuery(string sqlQuery)
        {
            long recordNum;
            OpenConnection();
            recordNum = _DBConnection.ExecuteNonQuery(sqlQuery);
            CloseConnection();
            return recordNum;
        }

        public object GetConnectionObject()
        {
            return _DBConnection.getConnectionObject();
        }

        public bool isOpen()
        {
            return _DBConnection.isOpen();
        }

        public bool isClose()
        {
            return _DBConnection.isClosed();
        }

        public T GetSingleValue<T>(string tableName, string columnName, string where)
        {
            try
            {
                DataTable dt = Select(string.Format("SELECT {0} FROM {1} WHERE {2}", columnName, tableName, where));
                if (dt == null || dt.Rows.Count == 0) return default(T);
                DataRow dr = dt.Rows[0];

                if (dr[columnName] is T)
                {
                    return (T)dr[columnName];
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(dr[columnName], typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        public DataTable GetAllTablesInDB(string DataBaseName)
        {
            switch (DBType)
            {
                case DBTypes.SQLServer:
                    return Select("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + DataBaseName + "'");
                case DBTypes.MySQL:
                    return Select("select table_name from information_schema.tables where TABLE_TYPE = 'BASE TABLE' AND Table_schema = '" + DataBaseName + "'");
                case DBTypes.Oracle:
                    return Select("SELECT table_name FROM user_tables");
                case DBTypes.PostgreSQL:
                    return Select("select table_name from information_schema.tables where table_schema = '" + DataBaseName + "'");
            }
            return null;
        }
    }
}
