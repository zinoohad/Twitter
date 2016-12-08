using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnections.DataBaseTypes
{
    class OracleConnection : SQLMethods
    {
        //The Oracle file that contains all the TNS details : C:\oracle\product\11.2.0\client_1\network\admin\tnsnames.ora
        //private Oracle.ManagedDataAccess.Client.OracleConnection connection = null;
        public OracleConnection(string host, string serviceName, string userName, string password,string port)
        {
            this.server = host;
            this.userName = userName;
            this.password = password;
            this.port = port;
            this.database = serviceName;
            ConnectionString = string.Format("Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1})))"
                         + "(CONNECT_DATA = (SERVICE_NAME = {2})));User Id={3};Password={4};",host,port,serviceName,userName,password);
            connection = new Oracle.ManagedDataAccess.Client.OracleConnection(ConnectionString);
            
        }

        public override DataTable Select(string sqlQuery)
        {
            DataTable dt = new DataTable();
            OracleCommand cmd = new OracleCommand(sqlQuery, (Oracle.ManagedDataAccess.Client.OracleConnection)connection);
            if (!OpenConnection())
            {
                SelectErrorMessage = "Can't connect to database, please check the connection and try again.";
                return dt;
            }    

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (OracleException e)
            {
                SelectErrorMessage = e.Message;
                return null;
            }
            return dt;
        }
        public override int Insert(string sqlQuery)
        {
            int rowsUpdated = 0;
            OracleCommand cmd = new OracleCommand(sqlQuery, (Oracle.ManagedDataAccess.Client.OracleConnection)connection);
            try
            {
                OpenConnection();
                rowsUpdated = cmd.ExecuteNonQuery();
                return rowsUpdated;
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                if (rowsUpdated == 0)
                    InsertMessage = "Record not inserted";
                else
                    InsertMessage = "Success!";
            }
            
        }
        public override int ExecuteNonQuery(string sqlQuery)
        {
            int rowsUpdated = 0;
            using (OracleCommand cmd = new OracleCommand(sqlQuery, (Oracle.ManagedDataAccess.Client.OracleConnection)connection))
            {
                try
                {
                    OpenConnection();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    return rowsUpdated;
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException(e.Message);
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
