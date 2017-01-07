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
        public SQLServerConnection(string serverAddress, string dataBase, string userName, string password, bool onLocalHost = false)
        {
            server = serverAddress;
            database = dataBase;
            this.userName = userName;
            this.password = password;
            string ConnectionString;
            if(!onLocalHost)
                ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", serverAddress, dataBase, userName, password);
            else 
                ConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI", serverAddress, dataBase);
            connection = new SqlConnection(ConnectionString);

        }
        public override DataTable Select(string sqlQuery)
        {
            SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection);
            DataTable dt = new DataTable();           
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public override int Insert(string sqlQuery) 
        {
            if (!sqlQuery.Contains("OUTPUT"))
                sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("values"), "OUTPUT Inserted.ID ");
            return ExecuteNonQuery(sqlQuery); 
        }
        public override int Update(string sqlQuery)
        {
            if (!sqlQuery.Contains("OUTPUT"))
                sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("into"), "OUTPUT Inserted.ID ");
            return ExecuteNonQuery(sqlQuery);
        }
        public override int Delete(string sqlQuery)
        {
            if (!sqlQuery.Contains("OUTPUT"))
                sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("where"), "OUTPUT Deleted.ID ");
            return ExecuteNonQuery(sqlQuery);
        }
        public override int ExecuteNonQuery(string sqlQuery)
        {
            int rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {     
                OpenConnection();
                try
                {
                    rowsUpdated = (int)cmd.ExecuteScalar();
                }
                catch { rowsUpdated = cmd.ExecuteNonQuery(); }
                return rowsUpdated;                
            }
        }
        
    }
}
