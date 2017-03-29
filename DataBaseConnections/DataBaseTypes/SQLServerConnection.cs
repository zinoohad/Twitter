using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System;

namespace DataBaseConnections.DataBaseTypes
{
    class SQLServerConnection : SQLMethods
    {
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
        public override long Insert(string sqlQuery, bool returnInsertedID = false, string columnName = "ID") 
        {
            if (returnInsertedID)
            {
                if (!sqlQuery.Contains("OUTPUT"))
                    sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("values"), string.Format("OUTPUT Inserted.{0} ",columnName));
                return ExecuteScalar(sqlQuery);
            }
            else return ExecuteNonQuery(sqlQuery); 
        }
        public override long Update(string sqlQuery, bool returnUpdatedID = false, string columnName = "ID")
        {
            if (returnUpdatedID)
            {
                if (!sqlQuery.Contains("OUTPUT") && sqlQuery.ToLower().Contains("into"))
                    sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("into"), string.Format("OUTPUT Inserted.{0} ", columnName));
                return ExecuteScalar(sqlQuery);
            }
            else return ExecuteNonQuery(sqlQuery); 
        }
        public override long Delete(string sqlQuery, bool returnDeletedID = false, string columnName = "ID")
        {
            if (returnDeletedID)
            {
                if (!sqlQuery.Contains("OUTPUT"))
                    sqlQuery = sqlQuery.Insert(sqlQuery.ToLower().IndexOf("where"), string.Format("OUTPUT Deleted.{0} ", columnName));
                return ExecuteScalar(sqlQuery);
            }
            else return ExecuteNonQuery(sqlQuery); 
        }
        public override long ExecuteNonQuery(string sqlQuery)
        {
            long rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {     
                OpenConnection();
                rowsUpdated = (long)cmd.ExecuteNonQuery();
                return rowsUpdated;                
            }
        }
        public override long ExecuteScalar(string sqlQuery)
        {
            long rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {
                OpenConnection();
                if (sqlQuery.Contains("OUTPUT"))
                {
                    object returnObj = cmd.ExecuteScalar();
                    rowsUpdated = long.Parse(returnObj.ToString());
                }
                else rowsUpdated = (long)cmd.ExecuteNonQuery();
                return rowsUpdated;
            }
        }
    }
}
