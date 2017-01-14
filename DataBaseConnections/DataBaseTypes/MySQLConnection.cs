﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataBaseConnections;

namespace DataBaseConnections.DataBaseTypes
{
    class MySQLConnection : SQLMethods
    {
        //private MySqlConnection connection = null;

        //Constructor
        public MySQLConnection(string serverAddress, string dataBase, string userName, string password)
        {
            ConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverAddress, userName, password, dataBase);
            server = serverAddress;
            database = dataBase;
            this.userName = userName;
            this.password = password;
            connection = new MySqlConnection(ConnectionString);           
        }

        //Insert statement
        public override long Insert(string sqlQuery, bool returnInsertedID = false, string columnName = "ID") { return ExecuteNonQuery(sqlQuery); }
        public override long Update(string sqlQuery, bool returnUpdatedID = false, string columnName = "ID") { return ExecuteNonQuery(sqlQuery); }
        public override long ExecuteNonQuery(string sqlQuery)
        {
            long rowsUpdated = 0;
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, (MySqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = (long)cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (Exception e)
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
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, (MySqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = (long)cmd.ExecuteScalar();
                    return rowsUpdated;
                }
                catch (Exception e)
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
        public override DataTable Select(string sqlQuery)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, (MySqlConnection)connection);
            if (!OpenConnection())
            {
                SelectErrorMessage = "Can't connect to database, please check the connection and try again.";
                return dt;
            }    
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (MySqlException e)
            {
                SelectErrorMessage = e.Message;
                return null;
            }
            return dt;
        }
       
    }
}
