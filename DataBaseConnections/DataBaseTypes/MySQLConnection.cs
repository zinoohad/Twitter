using System;
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
        public override int Insert(string sqlQuery)
        {
            int rowsUpdated = 0;
            //create command and assign the query and connection from the constructor
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, (MySqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    //Execute command
                    rowsUpdated = cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (Exception e)
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
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, (MySqlConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = cmd.ExecuteNonQuery();
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
        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = (MySqlConnection)connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
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
