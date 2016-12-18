using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DataBaseConnections;
using System.Data;
using Twitter.Classes;

namespace TwitterCollector.Common
{
    public class DBHandler
    {
        #region Params
        private DBConnection db;
        private static object tweetLocker = new object();
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
        /// <summary>
        /// Get all the database active subject
        /// </summary>
        /// <returns>Dictionary with the subjects ID and name.</returns>
        public DataTable GetActiveSubjects()
        {            
            DataTable dt = Select("SELECT * FROM Subject");
            return dt;
        }
        /// <summary>
        /// Get all keywords belongs to subject.
        /// </summary>
        /// <param name="subjectID">The subject id from DB.</param>
        /// <returns>Keys and values from DB.</returns>
        public Dictionary<int, string> GetSubjectKeywords(int subjectID)
        {
            Dictionary<int, string> keywords = new Dictionary<int, string>();
            DataTable dt = Select(string.Format("SELECT * FROM ViewActiveSubjects WHERE ID = {0}", subjectID));
            if (dt == null) return keywords;
            foreach (DataRow dr in dt.Rows)
                keywords.Add(int.Parse(dr["KeywordID"].ToString()), dr["KeyWord"].ToString());
            return keywords;
        }
        public List<Tweet> GetTweetsWithLock(long userID)
        {
            lock (tweetLocker)
            {

            }
        }
        #endregion
        #region Insert
        public bool AddPositiveWord(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            string query = string.Format("INSERT INTO DictionaryPositiveNegative (Word, IsPositive) VALUES ('{0}', 1)", word);
            try
            {
                Insert(query);
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
                Insert(query);
                return true;
            }
            catch { return false; }
        }
        public void SaveTweets(List<Tweet> tweets)
        {
            foreach (Tweet tweet in tweets)
            {
                User user = tweet.user;
                Hashtag[] hashtag = tweet.entities.hashtags;

            }
            //Need to save user first
        }
        public void SaveUsers(List<User> tweets)
        {

        }
        #endregion
        #region Update
        public bool UpdateSubjectStatus(int subjectID, bool status)
        {
            string query = string.Format("UPDATE Subject SET StartNewSubject = '{0}' WHERE ID = {1}", status, subjectID);
            try
            {
                db.Update(query);
                return true;
            }
            catch { return false; }
        }
        #endregion

    }
}
