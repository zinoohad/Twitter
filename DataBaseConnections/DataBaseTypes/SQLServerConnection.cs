using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System;

namespace DataBaseConnections.DataBaseTypes
{
    class SQLServerConnection : SQLMethods
    {
        //private SqlConnection connection = null;
        public SQLServerConnection(string serverAddress, string dataBase, string userName, string password)
        {
            server = serverAddress;
            database = dataBase;
            this.userName = userName;
            this.password = password;
            string ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", serverAddress, dataBase, userName, password);
            connection = new SqlConnection(ConnectionString);

        }
        public override DataTable Select(string sqlQuery)
        {
            SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection);
            DataTable dt = new DataTable();           
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public override int Insert(string sqlQuery) { return ExecuteNonQuery(sqlQuery); }
        public override int Update(string sqlQuery) { return ExecuteNonQuery(sqlQuery); }        
        public override int ExecuteNonQuery(string sqlQuery)
        {
            int rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {
                OpenConnection();
                rowsUpdated = cmd.ExecuteNonQuery();
                return rowsUpdated;                
            }
        }
        
    }
}
