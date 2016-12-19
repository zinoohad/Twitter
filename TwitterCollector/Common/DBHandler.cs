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
            return Update(query);
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
        public bool IncTweetsKeywordCounter(int keywordID)
        {
            try
            {
                string query = string.Format("UPDATE SubjectKeywords SET Count = CAST(Count AS INT) + 1 WHERE ID = {0}", keywordID);
                Update(query);
                return true;
            }
            catch(Exception e) { new TwitterException(e); return false; }
        }
        public bool IncSubjectUsersBelongCounter(int keywordID)
        {
            try
            {
                int subjectID = (int)GetSingleValue("SubjectKeywords", "SubjectID", string.Format("ID = {0}", keywordID));
                string query = string.Format("UPDATE Subject SET UsersBelong = CAST(UsersBelong AS INT) + 1 WHERE ID = {0}", subjectID);
                Update(query);
                return true;
            }
            catch (Exception e) { new TwitterException(e); return false; }
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
        //public List<Tweet> GetTweetsWithLock(long userID)
        //{
        //    lock (tweetLocker)
        //    {

        //    }
        //}
        public object GetSingleValue(string tableName, string columnName, string where)
        {
            try
            {
                DataTable dt = Select(string.Format("SELECT {0} FROM {1} WHERE {2}", columnName, tableName, where));
                if (dt == null || dt.Rows.Count == 0) return null;
                DataRow dr = dt.Rows[0];
                return dr[columnName];
            }
            catch
            {
                return null;
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
        public void SaveTweet(Tweet tweet, int? keywordID = null)
        {
            User user = tweet.user;
            Hashtag[] hashtag = tweet.entities == null ? null : tweet.entities.hashtags != null ? tweet.entities.hashtags : null;
            long userID, tweetID;
            int? placeID = null;
            int hashtagID;
            bool hasHashtag = hashtag.Length > 0 ? true : false;
            if(keywordID != null) tweet.keywordID = keywordID;
            // Insert User
            try
            {
                int accountAgeInMonth = (int)((DateTime.Now - DateTime.Parse(user.created_at)).TotalDays / 30);
                userID = (long)Insert(string.Format(@"INSERT INTO Users (ID,Name,ScreenName,CreateDate,Language,FollowersCount,FriendsCount,Location,TimeZone,Description,AccountAge) 
                                                                VALUES ({0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}',{10})", 
                                                                user.id, user.name, user.screen_name, user.created_at, user.lang, user.followers_count,
                                                                user.friends_count, user.location, user.time_zone, user.description, accountAgeInMonth));
                if (tweet.keywordID != null) IncSubjectUsersBelongCounter((int)tweet.keywordID);
                IncUsersCount();
            }
            catch { userID = user.id; }
            // Insert Place
            if (tweet.place != null)
            {
                Place p = tweet.place;
                try
                {

                    object tmpResult = GetSingleValue("Places", "ID", string.Format("PlaceID = '{0}'", p.id));
                    if (tmpResult == null)
                    {
                        placeID = Insert(string.Format(@"INSERT INTO Places (PlaceID, Country, CountryCode, FullCountryName, Name, PlaceType, Url) 
                                                    VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", p.id, p.country, p.country_code, p.full_name, p.name, p.place_type, p.url));
                    }
                    else placeID = (int)tmpResult;
                }
                catch { }
            }
            
            
            // Insert Tweet
            try
            {
                tweetID = (long)Insert(string.Format(@"INSERT INTO Tweets (ID,Date,Text,Language,RetweetCount,GeoLocation,UserID,PlaceID,TweetLength,HasHashtags,SubjectKeyword) 
                              VALUES ({0},'{1}','{2}','{3}',{4},'{5}',{6},{7},{8},'{9}',{10})"
                                ,tweet.id,tweet.created_at,tweet.text,tweet.lang, tweet.retweet_count,tweet.geo,userID,placeID == null ? "'NULL'" : placeID.ToString(),
                                tweet.text.Length, hasHashtag.ToString(), tweet.keywordID == null ? "'NULL'" : tweet.keywordID.ToString()));
                if (tweet.keywordID != null) IncTweetsKeywordCounter((int)tweet.keywordID);
                IncTweetsCount();
            }
            catch { tweetID = tweet.id; }
            // Insert Hashtags
            foreach (Hashtag h in hashtag)
            {
                try
                {
                    object tmpResult = GetSingleValue("Hashtags", "ID", string.Format("Name = '{0}'", h.text));
                    if(tmpResult == null)
                    {
                        hashtagID = Insert(string.Format("INSERT INTO Hashtags (Name) VALUES ({0})", h.text));
                    }
                    else hashtagID = (int)tmpResult;
                    Insert(string.Format("INSERT INTO TweetHashtags (TweetID,HashtagID,UserID) VALUES ({0},{1},{2})",tweetID ,hashtagID ,userID));
                }
                catch { }
            }
            if (tweet.retweeted_status != null) SaveTweet(tweet.retweeted_status, tweet.keywordID); // Insert the inner tweet to DB
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
