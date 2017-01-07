using DataBaseConnections.DataBaseTypes;
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
        private string _DBType;
        public string DBType { get { return _DBType; } }
        private string Server;
        private string Port;
        private string UserID;
        private string Password;
        private string _Database;
        public string Database { get { return _Database; } }
        public string ConnectionErrorMessage { get { return _DBConnection.ConnectionErrorMessage; } }
        public string InsertMessage { get { return _DBConnection.InsertMessage; } }
        public string SelectErrorMessage { get { return _DBConnection.SelectErrorMessage; } }
        public string ExecuteMessage { get { return _DBConnection.ExecuteMessage; } }
        public ConnectionState State { get { return _DBConnection.State; } }
        public string ConnectionString { get { return _DBConnection.ConnectionString; } }
        private SQLMethods _DBConnection = null;

        public DBConnection(DBTypes DBType, string Server, string Port, string UserID, string Password, string Database,bool onLocalHost = false)
        {
            this._DBType = "";
            this.Server = Server;
            this.Port = Port;
            this.UserID = UserID;
            this.Password = Password;
            this._Database = Database;
            switch (DBType)
            {
                case DBTypes.SQLServer:
                    this._DBType = "SQL Server";
                    _DBConnection = new SQLServerConnection(Server, Database, UserID, Password, onLocalHost);
                    break;
                case DBTypes.MySQL:
                    this._DBType = "MySQL";
                    _DBConnection = new MySQLConnection(Server, Database, UserID, Password);
                    break;
                case DBTypes.Oracle:
                    this._DBType = "Oracle";
                    _DBConnection = new OracleConnection(Server, Database, UserID, Password, Port);
                    break;
                case DBTypes.PostgreSQL:
                    this._DBType = "PostgreSQL";
                    _DBConnection = new PostgreSQLConnection(Server, Database, Port, UserID, Password);
                    break;
            }
        }
        public DBConnection(string DBType, string Server, string Port, string UserID, string Password, string Database)
        {
            this._DBType = DBType;
            this.Server = Server;
            this.Port = Port;
            this.UserID = UserID;
            this.Password = Password;
            this._Database = Database;
            switch (DBType)
            {
                case "SQL Server":
                    _DBConnection = new SQLServerConnection(Server, Database, UserID, Password);
                    break;
                case "MySQL":
                    _DBConnection = new MySQLConnection(Server, Database, UserID, Password);
                    break;
                case "Oracle":
                    _DBConnection = new OracleConnection(Server, Database, UserID, Password, Port);
                    break;
                case "PostgreSQL":
                    _DBConnection = new PostgreSQLConnection(Server, Database, Port, UserID, Password);
                    break;
            }
        }

        private bool CloseConnection()
        {
            return _DBConnection.CloseConnection();
        }
        private bool OpenConnection()
        {
            return _DBConnection.OpenConnection();
        }
        public DataTable Select(string sqlQuery)
        {
            DataTable dt;
            OpenConnection();
            dt = _DBConnection.Select(sqlQuery);
            CloseConnection();
            return dt;
        }
        public int Insert(string sqlQuery)
        {
            int recordNum;
            OpenConnection();
            recordNum = _DBConnection.Insert(sqlQuery);
            CloseConnection();
            return recordNum;
        }
        public int Update(string sqlQuery)
        {
            int recordNum;
            OpenConnection();
            recordNum = _DBConnection.Update(sqlQuery);
            CloseConnection();
            return recordNum;
        }
        public int Delete(string sqlQuery)
        {
            int recordNum;
            OpenConnection();
            recordNum = _DBConnection.Delete(sqlQuery);
            CloseConnection();
            return recordNum;
        }
        public int ExecuteNonQuery(string sqlQuery)
        {
            int recordNum;
            OpenConnection();
            recordNum = _DBConnection.ExecuteNonQuery(sqlQuery);
            CloseConnection();
            return recordNum;
        }
        public object getConnectionObject()
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
        public DataTable GetAllTablesInDB(string DataBaseName)
        {
            switch (DBType)
            {
                case "SQL Server":
                    return Select("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + DataBaseName + "'");
                case "MySQL":
                    return Select("select table_name from information_schema.tables where TABLE_TYPE = 'BASE TABLE' AND Table_schema = '" + DataBaseName + "'");
                case "Oracle":
                    return Select("SELECT table_name FROM user_tables");
                case "PostgreSQL":
                    return Select("select table_name from information_schema.tables where table_schema = '" + DataBaseName + "'");
            }
            return null;
        }
    }
}
