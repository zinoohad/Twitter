using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnections
{
    class ScriptsLoader
    {
        private bool _hasConnection = false;
        private SqlConnection _sqlConnection;
        private ServerConnection _serverConnection;
        private Server _server;
        private Database _db = new Database();
        private Scripter _scripter;
        private StringBuilder _stringBuilder = new StringBuilder();
        private Urn[] smoObjects = new Urn[1];
        private string _ErrorMessage;

        public string ErrorMessage { get { return _ErrorMessage; } }
        public bool hasConnection { get { return _hasConnection; } }

        public ScriptsLoader(SqlConnection connection)
        {
            setSqlConnection(connection);                         
        }

        public string ImportTableScript(string dataBaseName, string tableName, ref string errorMes)
        {
            errorMes = "";
            if (_hasConnection)
            {
                if (string.IsNullOrEmpty(dataBaseName.Trim()) || string.IsNullOrEmpty(tableName.Trim()))
                {
                    errorMes = "Entered empty values, check table and data base name.";
                    return null;
                }
                _db = _server.Databases[dataBaseName];
                if (_db == null)
                {
                    errorMes = string.Format("Can't find '{0}' database in current server.", dataBaseName);
                    return null;
                }
                TableCollection tc = _db.Tables;
                foreach (Table t in tc)
                {
                    smoObjects[0] = t.Urn;
                    if (t.IsSystemObject == false && t.Name.ToLower().Equals(tableName.ToLower()))
                    {
                        StringCollection sc = _scripter.Script(smoObjects);
                        foreach (var st in sc)
                        {
                            _stringBuilder.AppendLine(st);
                            _stringBuilder.AppendLine("GO");
                        }
                        return _stringBuilder.ToString();
                    }
                }
                errorMes = string.Format("Can't find '{0}' table in DB.",tableName);
            }
            else errorMes = "Can't create connection to server, please check the SqlConnection object and try again.";
            return null;
        }
        public string ImportViewScript(string dataBaseName, string viewName, ref string errorMes)
        {
            errorMes = "";
            if (_hasConnection)
            {
                if (string.IsNullOrEmpty(dataBaseName.Trim()) || string.IsNullOrEmpty(viewName.Trim()))
                {
                    errorMes = "Entered empty values, check view and data base name.";
                    return null;
                }
                _db = _server.Databases[dataBaseName];
                if (_db == null)
                {
                    errorMes = string.Format("Can't find '{0}' database in current server.", dataBaseName);
                    return null;
                }
                ViewCollection vc = _db.Views;
                foreach (Microsoft.SqlServer.Management.Smo.View v in vc)
                {
                    smoObjects[0] = v.Urn;
                    if (v.IsSystemObject == false && v.Name.ToLower().Equals(viewName.ToLower()))
                    {
                        StringCollection sc = _scripter.Script(smoObjects);
                        foreach (var st in sc)
                        {
                            _stringBuilder.AppendLine(st);
                            _stringBuilder.AppendLine("GO");
                        }
                        return _stringBuilder.ToString();
                    }
                }
                errorMes = string.Format("Can't find '{0}' view in DB.", viewName);
            }
            else errorMes = "Can't create connection to server, please check the SqlConnection object and try again.";
            return null;
        }
        public bool ExecuteScript(string sqlScript)
        {
            try
            {
                _server.ConnectionContext.ExecuteNonQuery(sqlScript);
                return true;
            }
            catch (SqlException e)
            {
                _ErrorMessage = e.Message;
                return false;
            }
        }
        public bool setSqlConnection(SqlConnection connection)
        {
            _hasConnection = checkSqlConnection(connection);
            if (_hasConnection)
            {
                _sqlConnection = connection;
                _serverConnection = new ServerConnection(connection);
                _server = new Server(_serverConnection);
                _scripter = new Scripter(_server);
                _scripter.Options.AllowSystemObjects = false;
                _scripter.Options.DriAllConstraints = false;
                //_scripter.Options.AnsiFile = false;
                //_scripter.Options.ScriptDrops = false;
                //_scripter.Options.WithDependencies = true;
                //_scripter.Options.IncludeHeaders = true;
                return true;
            }
            else return false;
        }
        private bool checkSqlConnection(SqlConnection con)
        {
            try
            {
                con.Open();
                return true;
            }
            catch { return true; }
        }
    }
}
