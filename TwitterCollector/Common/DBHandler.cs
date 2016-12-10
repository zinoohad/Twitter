using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DataBaseConnections;
using System.Data;

namespace TwitterCollector.Common
{
    public class DBHandler
    {
        #region Params
        private DBConnection db;
        #endregion
        #region Constructors
        public DBHandler() { db = new DBConnection(DBTypes.SQLServer, "localhost", "", "", "", "Twitter", true); }
        public DBHandler(DBConnection db) { this.db = db; }
        #endregion
        #region General
        public object GetValueByKey(string key)
        {
            string selectQuery = string.Format("SELECT Value FROM Settings WHERE [Key] = '{0}'", key);
            DataTable dt = db.Select(selectQuery);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return dr.IsNull("Value") ? null : dr["Value"];
        }
        public int SetValueByKey(string key, object value)
        {
            string query;
            if (GetValueByKey(key) != null) //Update record
            {
                query = string.Format("UPDATE Settings SET Value = '{0}' WHERE [Key] = '{1}'", value, key);
                try
                {
                    return db.Update(query);
                }
                catch { return 0; }
            }
            else
            {
                query = string.Format("INSERT INTO Settings VALUES ('{0}','{1}')", key, value);
                try
                {
                    return db.Insert(query);
                }
                catch { return 0; }
            }
        }
        private int UpdateSettingsCount(string key, long? value = null)
        {
            string query;
            if (value != null) query = string.Format("UPDATE Settings SET Value = '{0}' WHERE [Key] = '{1}'", value, key);
            else query = string.Format("UPDATE Settings SET Value = CAST(Value AS INT) + 1 WHERE [Key] = '{0}'", key);
            return db.Update(query);
        }
        public bool IncTweetsCount(long? value = null)
        {
            try
            {
                UpdateSettingsCount("TweetsCount",value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IncUsersCount(long? value = null)
        {
            try
            {
                UpdateSettingsCount("UsersCount",value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private DataTable Select(string query) { return db.Select(query); }
        private int Update(string query) { return db.Update(query); }
        private int Insert(string query) { return db.Insert(query); }
        #endregion
        #region Select
        public List<string> GetActiveSubjects()
        {
            List<string> keywords = new List<string>();
            DataTable dt = Select("SELECT * FROM ViewActiveSubjects");
            if(dt == null) return keywords;
            foreach (DataRow dr in dt.Rows)
                keywords.Add(dr["KeyWord"].ToString());
            return keywords;
        }
        #endregion
        #region Insert
        public bool AddPositiveWord(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            string query = string.Format("INSERT INTO DictionaryPositiveNegative (Word, IsPositive) VALUES ('{0}', 1)", word);
            try
            {
                db.Insert(query);
                return true;
            }
            catch { return false; }
        }
        public bool AddNegativeWord(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            string query = string.Format("INSERT INTO DictionaryPositiveNegative (Word, IsPositive) VALUES ('{0}', 0)", word);
            try
            {
                db.Insert(query);
                return true;
            }
            catch { return false; }
        }
        #endregion
        #region Update
        #endregion

    }
}
