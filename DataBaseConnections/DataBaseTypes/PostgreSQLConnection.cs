using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnections.DataBaseTypes
{
    class PostgreSQLConnection : SQLMethods
    {
        //private NpgsqlConnection connection = null;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public PostgreSQLConnection(string serverAddress, string dataBase, string port, string userName, string password)
        {
            server = serverAddress;
            database = dataBase;
            this.port = port;
            this.userName = userName;
            this.password = password;
            ConnectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                        serverAddress, port, userName, password, dataBase);
            connection = new NpgsqlConnection(ConnectionString);           
        }

        public override DataTable Select(string sqlQuery)
        {
            // data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlQuery, (NpgsqlConnection)connection);
            if (!OpenConnection())
            {
                SelectErrorMessage = "Can't connect to database, please check the connection and try again.";
                return dt;
            }    
            // reset DataSet
            ds.Reset();
            try
            {
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
            }
            catch (NpgsqlException e)
            {
                SelectErrorMessage = e.Message;
                return null;
            }
            // since it C# DataSet can handle multiple tables, i'm will select first
            dt = ds.Tables[0];
            // return DataTable
            return dt;
        }
        public override long Insert(string sqlQuery, bool returnInsertedID = false, string columnName = "ID") { return ExecuteNonQuery(sqlQuery); }
        public override long Update(string sqlQuery, bool returnUpdatedID = false, string columnName = "ID") { return ExecuteNonQuery(sqlQuery); }
        public override long ExecuteNonQuery(string sqlQuery)
        {
            long rowsUpdated = 0;
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, (NpgsqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = (long)cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (NpgsqlException e)
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
        public override long ExecuteScalar(string sqlQuery)
        {
            long rowsUpdated = 0;
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, (NpgsqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = (long)cmd.ExecuteScalar();
                    return rowsUpdated;
                }
                catch (NpgsqlException e)
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
