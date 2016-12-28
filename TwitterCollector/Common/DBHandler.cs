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
        private int Delete(string query) { return db.ExecuteNonQuery(query); }
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
        public List<Tweet> GetTopTweets(Dictionary<int, string> keywords, int? topNumber = null)
        {
            List<Tweet> topTweets = new List<Tweet>();
            string keys = ""; int top;
            foreach (KeyValuePair<int, string> key in keywords)
                keys += key.Key.ToString() + ",";
            if (topNumber != null) top = (int)topNumber;
            else
            {
                object tmpTop = (int)GetValueByKey("TweetsNumberInOnePull");
                if (tmpTop == null) top = 100;
                else top = (int)tmpTop;
            }
            keys = keys.Substring(0, keys.Length - 1);
            string query = string.Format("SELECT TOP {0} * FROM ViewTweetsConnectToSubject WHERE SubjectKeyword IN ({0}) ORDER BY [RetweetCount]+[FavoritesCount]", top, keys);
            DataTable dt = Select(query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    topTweets.Add(new Tweet(dr));
            }
            return topTweets;
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
        public long InsertUser(User user)
        {
            long userID = 0;
            try
            {
                int accountAgeInMonth = (int)((DateTime.Now - DateTime.Parse(user.created_at)).TotalDays / 30);
                userID = (long)Insert(string.Format(@"INSERT INTO Users (ID,Name,ScreenName,CreateDate,Language,FollowersCount,FriendsCount,Location,TimeZone,Description,AccountAge) 
                                                                VALUES ({0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}',{10})",
                                                                user.id, user.name, user.screen_name, user.created_at, user.lang, user.followers_count,
                                                                user.friends_count, user.location, user.time_zone, user.description, accountAgeInMonth));
                IncUsersCount();
                return userID;
            }
            catch (Exception e) { new TwitterException(e); return userID; } 
        }
        public int? InsertPlace(Place place)
        {
            int? placeID = null;
            if (place != null)
            {
                try
                {
                    object tmpResult = GetSingleValue("Places", "ID", string.Format("PlaceID = '{0}'", place.id));
                    if (tmpResult == null)
                    {
                        placeID = Insert(string.Format(@"INSERT INTO Places (PlaceID, Country, CountryCode, FullCountryName, Name, PlaceType, Url) 
                                                    VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", place.id, place.country, place.country_code, place.full_name, place.name, place.place_type, place.url));
                    }
                    else placeID = (int)tmpResult;                    
                }
                catch (Exception e) { new TwitterException(e); return placeID; }               
            }
            return placeID;
        }
        public long InsertTweet(Tweet tweet, long userID, int? placeID)
        {
            long tweetID;
            Hashtag[] hashtag = tweet.entities == null ? null : tweet.entities.hashtags != null ? tweet.entities.hashtags : null;
            bool hasHashtag = hashtag != null && hashtag.Length > 0 ? true : false;
            try
            {
                tweetID = (long)Insert(string.Format(@"INSERT INTO Tweets (ID,Date,Text,Language,RetweetCount,FavoritesCount,UserID,PlaceID,TweetLength,HasHashtags,SubjectKeyword,RetweetID) 
                              VALUES ({0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},'{9}',{10},'{11}')"
                    , tweet.id, tweet.CreateAt, tweet.text, tweet.lang, tweet.retweet_count == 0 ? "'NULL'" : tweet.retweet_count.ToString(), tweet.favorite_count, userID, placeID == null ? "'NULL'" : placeID.ToString(),
                                tweet.text.Length, hasHashtag.ToString(), tweet.keywordID == null ? "'NULL'" : tweet.keywordID.ToString(), tweet.retweeted_status == null ? "'NULL'" : tweet.retweeted_status.id_str));
                if (tweet.keywordID != null) IncTweetsKeywordCounter((int)tweet.keywordID);
                IncTweetsCount();
            }
            catch(Exception e) { new TwitterException(e); tweetID = tweet.id; }
            return tweetID;
        }
        public void InsertHashtags(Hashtag[] hashtag, long userID, long tweetID)
        {
            int hashtagID;
            if (hashtag != null)
            {
                foreach (Hashtag h in hashtag)
                {
                    try
                    {
                        object tmpResult = GetSingleValue("Hashtags", "ID", string.Format("Name = '{0}'", h.text));
                        if (tmpResult == null)
                        {
                            hashtagID = Insert(string.Format("INSERT INTO Hashtags (Name) VALUES ({0})", h.text));
                        }
                        else hashtagID = (int)tmpResult;
                        Insert(string.Format("INSERT INTO TweetHashtags (TweetID,HashtagID,UserID) VALUES ({0},{1},{2})", tweetID, hashtagID, userID));
                    }
                    catch { }
                }
            }
        }
        public void SaveTweet(Tweet tweet, int? keywordID = null)
        {
            Hashtag[] hashtag = tweet.entities == null ? null : tweet.entities.hashtags != null ? tweet.entities.hashtags : null;
            long userID, tweetID;
            int? placeID = null;           
            if(keywordID != null) tweet.keywordID = keywordID;
            // Insert User
            userID = InsertUser(tweet.user);
            if (userID != 0)
            {
                if (tweet.keywordID != null) IncSubjectUsersBelongCounter((int)tweet.keywordID);
            }
            else userID = tweet.user.id;                      
            placeID = InsertPlace(tweet.place);
            tweetID = InsertTweet(tweet,userID,placeID);
            InsertHashtags(hashtag, userID, tweetID);            
            if (tweet.retweeted_status != null) SaveTweet(tweet.retweeted_status, tweet.keywordID); // Insert the inner tweet to DB - recursive
        }
        public void SaveUsers(List<User> tweets)
        {

        }
        public int AddRemoveSubject(Action a, int subjectID = 0, string subjectName = "")
        {
            DataTable dt;
            if (a == Action.ADD)
            {
                dt = Select(string.Format("SELECT * FROM Subject WHERE Subject = '{0}'", subjectName));
                if (dt == null || dt.Rows.Count == 0)
                {
                    subjectID = Insert(string.Format("INSERT INTO Subject (Subject,IsActive,UsersBelong,StartNewSubject) VALUES ('{0}','{1}',0,'{1}')", subjectName, "True"));
                    
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    subjectID = (int)dr["ID"];
                }
            }
            else if (a == Action.REMOVE)
            {
                if (subjectID != 0)
                {
                    subjectID = Delete(string.Format("DELETE FROM Subject WHERE ID = {0}",subjectID));
                }
            }
            return subjectID;
        }
        public int AddRemoveKeyword(Action a, int subjectID, int keywordID = 0, string keywordName = "")
        {
            DataTable dt;
            if (a == Action.ADD)
            {
                dt = Select(string.Format("SELECT * FROM SubjectKeywords WHERE SubjectID = {0} AND Keyword = '{1}'", subjectID,keywordName));
                if (dt == null || dt.Rows.Count == 0)
                {
                    keywordID = Insert(string.Format("INSERT INTO SubjectKeywords (SubjectID,Keyword,Count) VALUES ({0},'{1}',0)", subjectID, keywordName));
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    keywordID = (int)dr["ID"];
                }
            }
            else if (a == Action.REMOVE)
            {
                if (keywordID != 0)
                {
                    keywordID = Delete(string.Format("DELETE FROM SubjectKeywords WHERE ID = {0}", keywordID));
                }
            }
            return keywordID;
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
    public enum Action
    {
        ADD,
        REMOVE
    }
}
