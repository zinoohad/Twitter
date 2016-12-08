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
            if (!OpenConnection())
            {
                SelectErrorMessage = "Can't connect to database, please check the connection and try again.";
                return dt;
            }             
            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException e)
            {
                SelectErrorMessage = e.Message;
            }
            return dt;
        }
        public override int Insert(string sqlQuery)
        {
            int rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    if (rowsUpdated == 0)
                        InsertMessage = "Record not inserted";
                    else
                        InsertMessage = "Success!";
                }
            }
        }
        public override int ExecuteNonQuery(string sqlQuery)
        {
            int rowsUpdated = 0;
            using (SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    if (rowsUpdated == 0)
                        ExecuteMessage = "Can't execute query.";
                    else
                        ExecuteMessage = "Success!";
                }
            }
        }
        
    }
}
