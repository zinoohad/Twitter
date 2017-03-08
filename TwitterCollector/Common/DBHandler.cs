using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DataBaseConnections;
using System.Data;
using Twitter.Classes;
using TwitterCollector.Objects;

namespace TwitterCollector.Common
{
    public class DBHandler
    {
        #region Params
        private DBConnection db;
        public const string TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
        #endregion
        #region Constructors
        public DBHandler() { db = new DBConnection(DBTypes.SQLServer, "localhost", "", "", "", "Twitter", true); }
        public DBHandler(DBConnection db) { this.db = db; }
        #endregion
        #region General
        #region System Functions
        public object GetValueByKey(string key)
        {
            string selectQuery = string.Format("SELECT Value FROM Settings WHERE [Key] = '{0}'", key);
            DataTable dt = db.Select(selectQuery);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return dr.IsNull("Value") ? null : dr["Value"];
        }
        public long SetValueByKey(string key, object value)
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
        private long UpdateSettingsCount(string key, long? value = null)
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
                UpdateSettingsCount("TweetsCount", value);
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
                UpdateSettingsCount("UsersCount", value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion        
        public bool IncTweetsKeywordCounter(List<int> keywordIDs)
        {
            try
            {
                foreach (int key in keywordIDs)
                {
                    string query = string.Format("UPDATE SubjectKeywords SET Count = CAST(Count AS INT) + 1 WHERE ID = {0}", key);
                    Update(query);
                }
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
        private long Update(string query, bool returnInsertedID = false, string columnName = "ID") { return db.Update(query, returnInsertedID, columnName); }
        private long Insert(string query, bool returnUpdatedID = false, string columnName = "ID") { return db.Insert(query, returnUpdatedID, columnName); }
        private long Delete(string query, bool returnDeletedID = false, string columnName = "ID") { return db.Delete(query, returnDeletedID, columnName); }
        public string ReplaceQuote(string text) { return string.IsNullOrEmpty(text) ? "NULL" : "'"+text.Replace("'", "''")+"'"; }
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
        public bool SetSingleValue(string tableName, string columnName, long rowID, object value)
        {
            try
            {
                string sqlQuery = string.Format("UPDATE {0} SET {1} = {2} WHERE ID = {3}", tableName, columnName, value, rowID);
                db.Update(sqlQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Select

        #region Tweets Collector

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

        public List<Tweet> GetTopTweets(int subjectID, int? topNumber = null)
        {
            List<Tweet> topTweets = new List<Tweet>();
            int top;
            if (topNumber != null) top = (int)topNumber;
            else
            {
                object tmpTop = GetValueByKey("TweetsNumberInOnePull");
                if (tmpTop == null) top = 100;
                else top = int.Parse(tmpTop.ToString());
            }
            string query = string.Format("SELECT TOP {0} * FROM ViewTweetsConnectToSubject WHERE SubjectID = {1} ORDER BY [RetweetCount]+[FavoritesCount] DESC", top, subjectID);
            DataTable dt = Select(query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<int> keywordIDs = GetTweetKeywords(long.Parse(dr["ID"].ToString()));//TODO: Get all the tweet keywords from DB, FUNCTION READY
                    topTweets.Add(new Tweet(dr, keywordIDs));
                }
            }
            return topTweets;
        }

        public List<long> GetTopUsersIDRatingForZeroPoint(int subjectID, int? topRecords = null)
        {
            if (subjectID == 0) return null;
            string sqlQuery = @"SELECT TOP {0} A.*,B.UserRelevantTweets,A.UserAllTweets - B.UserRelevantTweets AS NotRelevantToSubject FROM (
                                SELECT UserID, Count(*) AS UserAllTweets FROM ViewAllTweetsWithRelatedSubject GROUP BY UserID
                                ) A
                                JOIN (
                                SELECT C.UserID, COUNT(*) AS UserRelevantTweets FROM TweetToKeyword A
                                LEFT OUTER JOIN SubjectKeywords B ON A.KeywordID = B.ID
                                LEFT OUTER JOIN Tweets C ON A.TweetID = C.ID
                                WHERE B.SubjectID = {1}
                                GROUP BY C.UserID ) B ON A.UserID = b.UserID
                                ORDER BY B.UserRelevantTweets DESC ,A.UserAllTweets DESC";
            if (topRecords == null)
            {
                if ((topRecords = (int)GetValueByKey("TopUsersForZeroPoint")) == null) //Get top from settings table
                    topRecords = 25;    //Set default value
            }
            sqlQuery = string.Format(sqlQuery, topRecords, subjectID);
            DataTable dt = Select(sqlQuery);
            if (dt == null || dt.Rows.Count == 0) return null;
            List<long> topUsers = new List<long>();
            foreach (DataRow dr in dt.Rows)
                topUsers.Add(long.Parse(dr["ID"].ToString()));
            return topUsers;
        }

        public List<Tweet> TopRatedNotRelatedSubjectTweet(int subjectID, params long[] userID)
        {
            string sqlQuery = @"SELECT A.* FROM Tweets A
                                RIGHT OUTER JOIN (
	                                Select Max(A.Rating) as Rating,A.UserID FROM
	                                (
		                                SELECT B.UserID,B.ID,B.RetweetCount+B.FavoritesCount AS Rating FROM Users A
		                                RIGHT OUTER JOIN ViewAllTweetsWithRelatedSubject B ON A.ID = B.UserID
		                                WHERE B.SubjectID IS NULL OR B.SubjectID <> {0}
	                                ) A
	                                GROUP BY A.UserID
                                ) B ON A.UserID = B.UserID AND (A.RetweetCount+A.FavoritesCount) = B.Rating 
                                WHERE A.UserID in ({1})
                                ORDER BY B.Rating DESC";
            string usersIDs = string.Join(",", userID);
            sqlQuery = string.Format(sqlQuery, subjectID, usersIDs);
            DataTable dt = Select(sqlQuery);
            if (dt == null || dt.Rows.Count == 0) return null;
            List<Tweet> tweets = new List<Tweet>();
            foreach (DataRow dr in dt.Rows)
                tweets.Add(Global.FillClassFromDataRow<Tweet>(dr, new Tweet()));
            return tweets;
        }

        /// <summary>
        /// The function return the iso 639-1 code from the keyword id in the DB.
        /// By default the function return "en" which is the english iso 639-1 code.
        /// </summary>
        /// <param name="keywordID">KeywordID</param>
        /// <returns>String in iso 639-1 format.</returns>
        public string GetLanguageCodeFromKeyword(int keywordID)
        {
            object result = GetSingleValue("SubjectKeywords", "LanguageCode", string.Format("ID = {0}", keywordID));
            result = GetSingleValue("Languages", "Code", string.Format("ID = {0}", result));
            if (result == null) return "en";
            return result.ToString().Trim();
        }

        #endregion

        #region Subject Manager

        /// <summary>
        /// Get all the database active subject
        /// </summary>
        /// <returns>Dictionary with the subjects ID and name.</returns>
        public DataTable GetActiveSubjects(bool JustActive = false)
        {
            string sqlQuery = "SELECT  A.*,B.Name AS LanguageName,B.Code AS LanguageCode FROM Subject A "
                            + "LEFT OUTER JOIN Languages B ON A.LanguageID = B.ID";
            DataTable dt;
            if (JustActive) dt = Select(sqlQuery + " WHERE IsActive = 'True'");
            else dt = Select(sqlQuery);
            return dt;
        }

        public DataTable GetSubjectKeywordsDT(int subjectID)
        {
            DataTable dt = Select(string.Format("SELECT * FROM ViewActiveSubjects WHERE ID = {0}", subjectID));
            return dt;
        }

        #endregion

        #region User Collector

        /// <summary>
        /// The function get all the users in the DB that not import all they tweets
        /// </summary>
        /// <returns></returns>
        public List<long> GetTopUncheckedUsers(int topNumber = 0)
        {
            List<long> topUsers = new List<long>();            
            if (topNumber == 0)
            {
                object tmpTop = GetValueByKey("TopUsersForUserCollectorThread");
                try
                {
                    if (tmpTop == null) topNumber = 25;
                    else topNumber = int.Parse(tmpTop.ToString());
                }
                catch { topNumber = 25; }
            }
            DataTable dt = Select(string.Format("SELECT TOP {0} ID FROM Users WHERE HasAllHistory = 'False' AND AlreadyChecked = 'False' ORDER BY [FollowersCount]+[FriendsCount] DESC",topNumber));
            foreach (DataRow dr in dt.Rows)
            {
                topUsers.Add((long)dr["ID"]);
            }
            return topUsers;
        }
        
        #endregion

        public List<int> GetTweetKeywords(long tweetID)
        {
            DataTable dt = Select(string.Format("SELECT * FROM TweetToKeyword WHERE TweetID = {0}", tweetID));
            if (dt == null || dt.Rows.Count == 0) return null;
            List<int> keys = new List<int>();
            foreach(DataRow dr in dt.Rows)
                keys.Add(int.Parse(dr["KeywordID"].ToString()));
            return keys;

        }

        public bool UserAlreadyExists(long userID)
        {
            if (userID == 0) return false;
            if (GetSingleValue("Users", "ID", string.Format("ID = {0}", userID)) == null) return false;
            return true;
        }

        #endregion

        #region Insert

        #region Tweets Collector

        public long InsertUser(User user)
        {
            long userID = 0;
            try
            {
                DateTime createdAt = DateTime.ParseExact(user.CreateDate, TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                int accountAgeInMonth = (int)((DateTime.Now - createdAt).TotalDays / 30);
                userID = (long)Insert(string.Format(@"INSERT INTO Users (ID,Name,ScreenName,CreateDate,Language,FollowersCount,FriendsCount,Location,TimeZone,Description,AccountAge,BackgroundImage,BannerImage,ProfileImage)" +
                                                               " VALUES ({0},{1},{2},'{3}','{4}',{5},{6},{7},'{8}',{9},{10},{11},{12},{13})",
                                                                user.ID, ReplaceQuote(user.Name), ReplaceQuote(user.ScreenName), createdAt.ToString("yyyy-MM-dd HH:mm:ss"), user.Language, user.FollowersCount,
                                                                user.FriendsCount, ReplaceQuote(user.Location), user.TimeZone, ReplaceQuote(user.Description), accountAgeInMonth,
                                                                ReplaceQuote(user.BackgroundImage), ReplaceQuote(user.BannerImage), ReplaceQuote(user.ProfileImage)), true);
                if (userID == 0)
                {
                    userID = user.ID;
                }
                else IncUsersCount();
                return userID;
            }
            catch (Exception e) { new TwitterException(e); return userID; }
        }

        public void SaveTweet(Tweet tweet, List<int> keywordID = null)
        {
            Hashtag[] hashtag = tweet.entities == null ? null : tweet.entities.hashtags != null ? tweet.entities.hashtags : null;
            long userID, tweetID;
            bool IsNewTweet = true;
            long? placeID = null;
            if (keywordID != null) tweet.keywordID = keywordID;
            // Insert User if not exists
            if (!UserAlreadyExists(tweet.user.ID))
            {
                userID = InsertUser(tweet.user);
            }
            else userID = tweet.user.ID;
            if (userID == 0)
                userID = tweet.user.ID;
            placeID = InsertPlace(tweet.place);
            tweetID = InsertTweet(tweet, userID, placeID, ref IsNewTweet);
            if (tweet.keywordID != null && tweet.keywordID.Count > 0)   // Create connection between tweet to keywords
            {
                if (IsNewTweet) //Just if the tweet is new, increment the counter
                {
                    foreach (int key in tweet.keywordID)    // Increment subject counter
                        IncSubjectUsersBelongCounter(key);
                }
                ConnectTweetToKeywords(tweet.ID, tweet.keywordID);     // Create connection between tweet to the belong keywords
            }
            InsertHashtags(hashtag, userID, tweetID);
            if (tweet.retweeted_status != null) SaveTweet(tweet.retweeted_status, tweet.keywordID); // Insert the inner tweet to DB - recursive
        }

        #endregion

        #region Subject Manager

        public int AddRemoveSubject(Action a, int subjectID = 0, string subjectName = "")
        {
            DataTable dt;
            if (a == Action.ADD)
            {
                dt = Select(string.Format("SELECT * FROM Subject WHERE Subject = '{0}'", subjectName));
                if (dt == null || dt.Rows.Count == 0)
                {
                    subjectID = (int)Insert(string.Format("INSERT INTO Subject (Subject,IsActive,UsersBelong,StartNewSubject) OUTPUT Inserted.ID VALUES ('{0}','{1}',0,'{1}')", subjectName, "True"), true);

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
                    subjectID = (int)Delete(string.Format("DELETE FROM Subject WHERE ID = {0}", subjectID), true);
                }
            }
            return subjectID;
        }

        public int AddRemoveKeyword(Action a, int subjectID, int keywordID = 0, string keywordName = "", int keywordLanguage = 84)
        {
            DataTable dt;
            if (a == Action.ADD)
            {
                dt = Select(string.Format("SELECT * FROM SubjectKeywords WHERE SubjectID = {0} AND Keyword = '{1}'", subjectID, keywordName));
                if (dt == null || dt.Rows.Count == 0)
                {
                    keywordID = (int)Insert(string.Format("INSERT INTO SubjectKeywords (SubjectID,Keyword,Count,LanguageID) VALUES ({0},'{1}',0,{2})", subjectID, keywordName, keywordLanguage), true);
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
                    keywordID = (int)Delete(string.Format("DELETE FROM SubjectKeywords WHERE ID = {0}", keywordID), true);
                }
            }
            return keywordID;
        }

        #endregion

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
        
        public long? InsertPlace(Place place)
        {
            long? placeID = null;
            if (place != null)
            {
                try
                {
                    object tmpResult = GetSingleValue("Places", "ID", string.Format("PlaceID = '{0}'", place.id));
                    if (tmpResult == null)
                    {
                        placeID = Insert(string.Format(@"INSERT INTO Places (PlaceID, Country, CountryCode, FullCountryName, Name, PlaceType, Url) "+
                                                    "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", place.id, place.country, place.country_code, place.full_name, place.name, place.place_type, place.url),true);
                        if (placeID == 0) placeID = 0;
                    }
                    else placeID = (int)tmpResult;                    
                }
                catch (Exception e) { new TwitterException(e); return placeID; }               
            }
            return placeID;
        }
        public long InsertTweet(Tweet tweet, long userID, long? placeID,ref bool IsNewTweet)
        {
            IsNewTweet = true;
            long tweetID;
            Hashtag[] hashtag = tweet.entities == null ? null : tweet.entities.hashtags != null ? tweet.entities.hashtags : null;
            bool hasHashtag = hashtag != null && hashtag.Length > 0 ? true : false;
            try
            {
                DateTime createdAt = DateTime.ParseExact(tweet.Date, TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                tweetID = (long)Insert(string.Format(@"INSERT INTO Tweets (ID,Date,Text,Language,RetweetCount,FavoritesCount,UserID,PlaceID,TweetLength,HasHashtags,SubjectKeyword,RetweetID) "+
                              "VALUES ({0},'{1}',{2},'{3}',{4},{5},{6},{7},{8},'{9}',{10},{11})"
                    , tweet.ID, createdAt.ToString("yyyy-MM-dd HH:mm:ss"), ReplaceQuote(tweet.Text), tweet.Language, tweet.RetweetCount, tweet.FavoritesCount, userID, placeID == null ? "NULL" : placeID.ToString(),
                                tweet.Text.Length, hasHashtag.ToString(), "NULL", tweet.retweeted_status == null ? "NULL" : tweet.retweeted_status.ID.ToString()),true);
                if (tweetID == 0)
                {
                    tweetID = tweet.ID;
                    IsNewTweet = false;
                }
                else IncTweetsCount();
                if (tweet.keywordID != null) IncTweetsKeywordCounter(tweet.keywordID);
            }
            catch(Exception e) { new TwitterException(e); tweetID = tweet.ID; }
            return tweetID;
        }
        public void InsertHashtags(Hashtag[] hashtag, long userID, long tweetID)
        {
            long hashtagID;
            if (hashtag != null)
            {
                foreach (Hashtag h in hashtag)
                {
                    try
                    {
                        object tmpResult = GetSingleValue("Hashtags", "ID", string.Format("Name = '{0}'", h.text));
                        if (tmpResult == null)
                        {
                            hashtagID = Insert(string.Format("INSERT INTO Hashtags (Name) VALUES ('{0}')", h.text),true);
                        }
                        else hashtagID = (int)tmpResult;
                        Insert(string.Format("INSERT INTO TweetHashtags (TweetID,HashtagID,UserID) VALUES ({0},{1},{2})", tweetID, hashtagID, userID));
                    }
                    catch (Exception e) { new TwitterException(e); }
                }
            }
        }
        
        public void ConnectTweetToKeywords(long tweetID, List<int> keywords)
        {
            foreach(int key in keywords)
                Insert(string.Format("INSERT INTO TweetToKeyword (TweetID,KeywordID) VALUES ({0},{1})",tweetID,key));
        }
        public void SaveUsers(List<User> users)
        {

        }
        
        #endregion

        #region Update

        #region Tweets Collector

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

        #region Subject Manager

        public bool UpdateKeywordLanguage(ref KeywordO keyword, string language)
        {
            DataTable dt = Select(string.Format("SELECT * FROM Languages WHERE Name = '{0}'", language));
            if (dt == null || dt.Rows.Count == 0) return false;
            DataRow dr = dt.Rows[0];
            if (!SetSingleValue("SubjectKeywords", "LanguageID", keyword.ID, dr["ID"])) return false;
            keyword.ID = (int)dr["ID"];
            keyword.LanguageName = dr["Name"].ToString();
            keyword.LanguageCode = dr["Code"].ToString();
            return true;
        }

        public bool UpdateSubjectLanguage(ref SubjectO subject, string language)
        {
            DataTable dt = Select(string.Format("SELECT * FROM Languages WHERE Name = '{0}'", language));
            if (dt == null || dt.Rows.Count == 0) return false;
            DataRow dr = dt.Rows[0];
            if (!SetSingleValue("Subject", "LanguageID", subject.ID, dr["ID"])) return false;
            subject.ID = (int)dr["ID"];
            subject.LanguageName = dr["Name"].ToString();
            subject.LanguageCode = dr["Code"].ToString();
            return true;
        }

        #endregion

        #endregion

    }
    public enum Action
    {
        ADD,
        REMOVE
    }
}
