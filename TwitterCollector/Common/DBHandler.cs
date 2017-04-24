using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseConnections;
using System.Data;
using Twitter.Classes;
using TwitterCollector.Objects;
using TopicSentimentAnalysis.Classes;
using Twitter.Common;
using TopicSentimentAnalysis;
using System.Threading;

namespace TwitterCollector.Common
{
    public class DBHandler
    {
        #region Params

        private DBConnection db;

        public const string TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public const string SqlServerDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        #endregion

        #region Constructors

        public DBHandler() { db = new DBConnection(DBTypes.SQLServer, "localhost", "", "", "", "Twitter", true); }

        public DBHandler(DBConnection db) { this.db = db; }

        #endregion
        
        #region General

        #region System Functions

        /// <summary>
        /// Return value from settings table by the given key.
        /// In case the key not exists and the defaultValue parametere has value, new record will be insert to the settings table with the given key and value.
        /// </summary>
        /// <param name="key">The string uniqe key to check in the settings table.</param>
        /// <param name="defaultValue">Optional: Create new record and return this value.</param>
        /// <returns>Value for the given key.</returns>
        public object GetValueByKey(string key, object defaultValue = null)
        {
            string selectQuery = string.Format("SELECT Value FROM Settings WHERE [Key] = '{0}'", key);
            DataTable dt = db.Select(selectQuery);
            if (dt == null || dt.Rows.Count == 0)
            {
                if (defaultValue != null)
                {
                    SetValueByKey(key, defaultValue);
                    return defaultValue;
                }
                else
                    return null;
            }
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
                query = string.Format("INSERT INTO Settings VALUES ('{0}','{1}',NULL)", key, value);
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

        public ApiKeys GetApiKey(ExternalAPI externalApi)
        {
            string sqlQuery = string.Format("SELECT TOP 1 * FROM ExternalApiKeys WHERE AccountName = '{0}' AND RemainingCredits > 0", externalApi.ToString());
            DataTable dt = Select(sqlQuery);
            if (dt == null || dt.Rows.Count == 0) 
                return null;
            DataRow dr = dt.Rows[0];
            return new ApiKeys((int)dr["ID"]
                , (ExternalAPI) Enum.Parse(typeof(ExternalAPI), dr["AccountName"].ToString())
                , (int)dr["RemainingCredits"]
                , dr["Key1"].ToString()
                , dr["Key2"].ToString()
                , dr["Key3"].ToString()
                , dr["Key4"].ToString()
                );
        }

        public void UpdateRemainingCredits(ref ApiKeys key)
        {
            key.RemainingCredits--;
            SetSingleValue("ExternalApiKeys", "RemainingCredits", key.ID, key.externalApi);
        }

        public void UpdateDictionaryAge()
        {
            string query = "SELECT ID,Word FROM DictionaryAge WHERE MostPositiveAgeGroup IS NULL OR MostNegativeAgeGroup IS NULL";
            DataTable dt = Select(query);

            if (dt == null || dt.Rows.Count == 0) return;
            string word;

            foreach (DataRow dr in dt.Rows)
            {
                word = dr["Word"].ToString();
                List<WordAge> wordAgeList = WordSentimentAnalysis.CheckWordAge(word);
                if (wordAgeList.Count == 0) 
                    continue;
                WordAge oneValue = wordAgeList[0];
            Update(string.Format(@"UPDATE DictionaryAge SET Age13To18 = {0}, Age19To22 = {1}, Age23To29 = {2}, Age30Plus = {3}
                , MostPositiveAgeGroup = {4}, MostNegativeAgeGroup = {5}, LastUpdate = '{6}' WHERE ID = {7}", oneValue.Age13To18, oneValue.Age19To22,
                            oneValue.Age23To29, oneValue.Age30Plus, oneValue.MostPositiveAgeGroup, oneValue.MostNegativeAgeGroup,DateTime.Now.ToString(SqlServerDateTimeFormat), dr["ID"].ToString()));
            }
        }

        public void WriteExceptionToDB(string fileName, string functionName, int lineNumber, string message)
        {
            string programMode = "Release";
#if (DEBUG)
            programMode = "Debug";
#endif
            string userAndMachineName = Environment.UserName + " (" + Environment.MachineName + ")";
            string sqlQuery = string.Format("INSERT INTO TwitterException (ProgramMode,UserAndMachineName,FileName,FunctionName,LineNumber,Message) "+
                                            "VALUES ('{0}','{1}','{2}','{3}',{4},'{5}')", programMode, userAndMachineName, fileName, functionName, lineNumber, message.Replace("'","''"));
            try
            {
                Update(sqlQuery);
            }
            catch { }
        }

        #endregion        

        #region Common Functions

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

        private DataTable Select(string query, params object[] args) 
        {
            query = string.Format(query, args);
            return db.Select(query); 
        }

        private long Update(string query, params object[] args) 
        {
            query = string.Format(query, args);
            return db.Update(query); 
        }

        private long Insert(string query, params object[] args)
        {
            query = string.Format(query, args);
            return db.Insert(query);
        }

        private long Delete(string query, params object[] args)
        {
            query = string.Format(query, args);
            return db.Delete(query);
        }
        
        public string ReplaceQuote(string text) { return string.IsNullOrEmpty(text) ? "NULL" : "'" + text.Replace("'", "''") + "'"; }

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

        public DataTable GetTable(string tableName, string where = null, string orderBy = null)
        {
            string sqlQuery = string.Format("SELECT * FROM {0}", tableName);
            if (where != null)
                sqlQuery += string.Format(" WHERE {0}", where);
            if (orderBy != null)
                sqlQuery += string.Format(" ORDER BY {0}", orderBy);
            return Select(sqlQuery);
        }

        #endregion
        
        #endregion

        #region Select

        #region Twitter

        public TwitterKeys GetTwitterKey()
        {
            string sqlQuery = "SELECT TOP 1 * FROM ExternalApiKeys WHERE AccountName = 'TwitterAPI' ORDER BY UpdateDate ASC";
            DataTable dt = Select(sqlQuery);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            TwitterKeys twitterKeys = new TwitterKeys(
                int.Parse(dr["ID"].ToString())
                ,dr["Key1"].ToString()
                ,dr["Key2"].ToString()
                ,dr["Key3"].ToString()
                ,dr["Key4"].ToString()
                );
            // Update use date
            sqlQuery = string.Format("UPDATE ExternalApiKeys SET UpdateDate = '{1}' WHERE ID = {0}", twitterKeys.ID, DateTime.Now.ToString(SqlServerDateTimeFormat));
            Update(sqlQuery);
            return twitterKeys;
        }

        #endregion

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
                object tmpTop = GetValueByKey("TweetsNumberInOnePull", 20);
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
                topRecords = int.Parse(GetValueByKey("TopUsersForZeroPoint", 25).ToString()); //Get top from settings table
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
                tweets.Add(Global.FillClassFromDataRow<Tweet>(dr));
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

        public List<KeywordO> GetSubjectKeywordsList(int subjectID)
        {
            List<KeywordO> keywordsList = new List<KeywordO>();
            DataTable dt = GetSubjectKeywordsDT(subjectID) ;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    keywordsList.Add(new KeywordO(dr));
            }
            return keywordsList;
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
                object tmpTop = GetValueByKey("TopUsersForUserCollectorThread",25);
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
            if (GetSingleValue("Users", "ID", string.Format("ID = {0}", userID)) == null) 
                return false;
            return true;
        }

        #region TweetPosNeg

        public DataTable FindPositiveNegativeWords(string[] splitSentence)
        {
            //TODO: Need to upgrade this function. 
            // We need to count the number of times the word appears in a sentence.
            // Need to check if two close words are a person name, and remove them.
            string orJoin = string.Join("', '", splitSentence);
            string sqlQuery = string.Format("SELECT * FROM DictionaryPositiveNegative WHERE IsEmoticon = 0 AND Word IN ('{0}')", orJoin);
            return Select(sqlQuery);
        }

        public List<Tweet> GetTweetsToCheckSentementAnalysis(ThreadType threadType, int? topNumber = null)
        {
            List<Tweet> topTweets = new List<Tweet>();
            int top;
            string query;
            if (topNumber != null) top = (int)topNumber;
            else
            {
                object tmpTop = GetValueByKey("TweetsNumberInOnePull",20);
                if (tmpTop == null) top = 100;
                else top = int.Parse(tmpTop.ToString());
            }

            if(threadType == ThreadType.SENTIMENT_ANALYSIS)
                query = string.Format("SELECT TOP {0} * FROM Tweets WHERE SentementAnalysisRank IS NULL AND RetweetID IS NULL AND Language = 'en'",top);
            else
                query = string.Format("SELECT TOP {0} * FROM Tweets WHERE Rank IS NULL AND RetweetID IS NULL AND Language = 'en'", top);

            DataTable dt = Select(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    topTweets.Add(new Tweet(dr));
                }
            }
            return topTweets;
        }

        /// <summary>
        /// Search for emoticons in split sentence
        /// </summary>
        /// <param name="splitSentence">Split sentence</param>
        /// <returns>DataTable with all the results.</returns>
        public DataTable FindEmoticons(string[] splitSentence)
        {
            splitSentence = splitSentence.Select(r => string.Format("Word LIKE '%{0}'", r)).ToArray();
            string orJoin = string.Join(" OR ", splitSentence);
            string sqlQuery = string.Format("SELECT * FROM DictionaryPositiveNegative WHERE IsEmoticon = 1 AND ({0})", orJoin);
            return Select(sqlQuery);
        }

        #endregion

        #region Tweet Age

        /// <summary>
        /// This function compare word to Age Dictionary
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Value of word weight</returns>
        public WordAge GetAgeValueByWord(string word)
        {
            WordAge wordage = null;
            string query = string.Format("SELECT * FROM DictionaryAge WHERE Word = '{0}' ",word);
            DataTable dt = Select(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                   wordage = Global.FillClassFromDataRow<WordAge>(dr);
                }
            }
            return wordage;
        }

        /// <summary>
        /// Search for emoticons for ages dictnionary
        /// </summary>
        /// <returns>DataTable with emoticons and value</returns>
        public List<WordAge> FindEmoticonsForAges()
        {
            List<WordAge> EmoticonsForAges = new List<WordAge>();
            string query = string.Format("SELECT * FROM DictionaryAge WHERE IsEmoticon = 1 ");
            DataTable dt = Select(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WordAge wordage = Global.FillClassFromDataRow<WordAge>(dr);
                    EmoticonsForAges.Add(wordage);
                }
            }
            return EmoticonsForAges;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadType"></param>
        /// <param name="topNumber"> </param>
        /// <returns></returns>
        public List<long> GetUserIDsToCheckAnalysisByAge(ThreadType threadType, int? topNumber = null)
        {
            List<long> topUsers = new List<long>();

            int top;
            if (topNumber != null) top = (int)topNumber;
            else
            {
                object tmpTop = GetValueByKey("TweetsNumberInOnePull",20);
                if (tmpTop == null) top = 100;
                else top = int.Parse(tmpTop.ToString());
            }
            string query = string.Format("SELECT TOP {0} * FROM UserProperties WHERE AgeGroupID IS NULL ORDER BY [RelevantTweetsCount] DESC", top);
            DataTable dt = Select(query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    long UserID = long.Parse(dr["UserID"].ToString());
                    topUsers.Add(UserID);
                }
            }
            return topUsers;
       
        }
               
        /// <summary>
        /// This function get all user's tweets by userID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Tweet> GetUserTweetByUserID(long userID)
        {
            List<Tweet> UserTweets = new List<Tweet>();

            List<long> topUsers = new List<long>();
    
            string query = string.Format("SELECT  * FROM Tweets WHERE UserID = {0} ORDER BY [RetweetCount]+[FavoritesCount] DESC", userID);
            DataTable dt = Select(query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UserTweets.Add(new Tweet(dr));      
                }
            }     
            return UserTweets;
        }

        #endregion

        #region Tweet Gender

        /// <summary>
        /// This function get all users that need to analyze Gender
        /// </summary>
        /// <param name="threadType"></param>
        /// <param name="topNumber"> </param>
        /// <returns></returns>
        public List<long> GetUserIDsToCheckAnalysisByGender(ThreadType threadType, int? topNumber = null)
        {
            List<long> topUsers = new List<long>();

            int top;
            if (topNumber != null) top = (int)topNumber;
            else
            {
                object tmpTop = GetValueByKey("TweetsNumberInOnePull", 20);
                if (tmpTop == null) top = 100;
                else top = int.Parse(tmpTop.ToString());
            }
            string query = string.Format("SELECT TOP {0} * FROM UserProperties WHERE GenderID IS NULL ORDER BY [RelevantTweetsCount] DESC", top);
            DataTable dt = Select(query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    long UserID = long.Parse(dr["UserID"].ToString());
                    topUsers.Add(UserID);
                }
            }
            return topUsers;

        }

        /// <summary>
        /// Search for emoticons for ages dictnionary
        /// </summary>
        /// <returns>DataTable with emoticons and value</returns>
        public List<WordGender> FindEmoticonsForGender()
        {
            List<WordGender> EmoticonsForGender = new List<WordGender>();
            string query = string.Format("SELECT * FROM DictionaryGender WHERE IsEmoticon = 1 ");
            DataTable dt = Select(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WordGender wordage = Global.FillClassFromDataRow<WordGender>(dr);
                    EmoticonsForGender.Add(wordage);
                }
            }
            return EmoticonsForGender;
        }

        /// <summary>
        /// This function compare word to Age Dictionary
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Value of word weight</returns>
        public double GetGenderValueByWord(string word)
        {
            double wordGender = 0;
            string query = string.Format("SELECT TOP 1 * FROM DictionaryGender WHERE Word = '{0}' ", word);
            DataTable dt = Select(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                    wordGender = double.Parse(dt.Rows[0]["WordRate"].ToString());
                
            }
            
            return wordGender;
        }
       
        public Dictionary<string,double> GetGenderDictionary()
        {
            Dictionary<string, double> genderDinctionaryTable = new Dictionary<string, double>();
            DataTable dt= Select("SELECT Word, WordRate FROM DictionaryGender ");
            foreach(DataRow dr in dt.Rows)
            {
                genderDinctionaryTable.Add(
                    dr["Word"].ToString(),
                    double.Parse(dr["WordRate"].ToString())
                    );
            }
            return genderDinctionaryTable;
        }

        #endregion

        #region Image Analysis

        public List<User> GetUsersToImageAnalysis()
        {
            List<User> users = new List<User>();
            int topUsersForImageAnalysis = int.Parse(GetValueByKey("TopUsersForImageAnalysis", 30).ToString());
            DataTable dt = Select(string.Format("SELECT TOP {0} * FROM ViewUsersForImageAnalysis WHERE ImageAnalysisChecked = 0 AND ProfileImage IS NOT NULL", topUsersForImageAnalysis));

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new User() {
                        ID = long.Parse(dr["UserID"].ToString())
                        ,ProfileImage = dr["ProfileImage"].ToString()
                        ,UserPropertiesID = int.Parse(dr["UserPropertiesID"].ToString())
                    } );
                }
            }
            return users;
            
        }

        #endregion

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
                                                                ReplaceQuote(user.BackgroundImage), ReplaceQuote(user.BannerImage), ReplaceQuote(user.ProfileImage.Replace("_normal",""))), true);
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
            if (keywordID != null)
            {
                tweet.keywordID = keywordID;
            }
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

                    // Remove subject threads
                    Delete("DELETE FROM ThreadsControl WHERE SubjectID = {0}", subjectID);
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

        /// <summary>
        /// Save tweet in the database.
        /// If the tweet is a retweet, save also the original tweet id.
        /// </summary>
        /// <param name="tweet">Tweet object</param>
        /// <param name="userID">Relevant user id</param>
        /// <param name="placeID">Place id</param>
        /// <param name="IsNewTweet">An referance to flag that check if the tweet already exists in the data base.</param>
        /// <returns>Tweet ID</returns>
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
                if (tweet.keywordID != null)
                {
                    IncTweetsKeywordCounter(tweet.keywordID);
                    IncrementUserRelevantTweetsCounter(tweet.keywordID, userID);
                }
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

        public void IncrementUserRelevantTweetsCounter(List<int> keywordsID, long userID)
        {
            string keywordIDs = string.Join(",",keywordsID.ToArray());
            DataTable dt = Select(string.Format("SELECT ID FROM ViewActiveSubjects WHERE KeywordID IN ({0})", keywordIDs));
            int[] subjectIDs = dt.AsEnumerable().Select(r => r.Field<int>("ID")).Distinct().ToArray();

            foreach (int subject in subjectIDs)
            {
                string sqlQuery = string.Format("SELECT ID FROM UserProperties WHERE UserID = {0} AND SubjectID = {1}", userID, subject);
                dt = Select(sqlQuery);
                if (dt != null && dt.Rows.Count > 0)
                {
                    // The record already exists
                    int recordID = int.Parse(dt.Rows[0]["ID"].ToString());
                    Update(string.Format("UPDATE UserProperties SET RelevantTweetsCount = CAST(RelevantTweetsCount AS INT) + 1 WHERE ID = {0}", recordID));
                }
                else
                {
                    //Need to insert new record
                    Insert("INSERT INTO UserProperties (UserID, SubjectID, RelevantTweetsCount) VALUES ({0},{1},{2})", userID, subject, 1);
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

        #region Sentiment Analysis

        public void SaveTweetScore(Tweet tweet, string score, int confidence)
        {
            // TODO: Save score for all the retweets 
            DataTable dt = Select(string.Format("SELECT ID FROM Tweets WHERE ID = {0} OR RetweetID = {0}", tweet.id_str));
            string[] ids = dt.AsEnumerable()
                            .Select(row => row["ID"].ToString())
                            .ToArray();
            Update(string.Format("UPDATE Tweets SET SentementAnalysisRank = '{0}', SentementAnalysisConfidence = {1} WHERE ID IN ({2})", score, confidence, string.Join(",", ids)));
        }

        public void LearnNewPosNegWord(object obj)
        {
            string word = null;
            bool isPositive = false;

            if (obj is PolarityTerm)
            {
                word = ((PolarityTerm)obj).text;
                isPositive = ((PolarityTerm)obj).In("P+", "P");
            }
            else if (obj is Concept)
            {
                word = ((Concept)obj).form;
                isPositive = ((Concept)obj).In("P+", "P");
            }

            bool isEmoticon = Global.IsEmoticon(word);

            //TODO: Split the word and check it => '(very) interesting'
            word = Global.GetStringWithoutPunctuation(word);

            DataTable dt = Select(string.Format("SELECT * FROM DictionaryPositiveNegative WHERE Word = '{0}'", word));          
            if (dt == null || dt.Rows.Count == 0)
            {
                //New word
                if (isPositive)
                    Insert(string.Format("INSERT INTO DictionaryPositiveNegative (Word,IsPositive,PositiveAppearanceCount,IsEmoticon) VALUES('{0}','{1}',1,'{2}')"
                        , word, isPositive.ToString(), isEmoticon.ToString()));
                else
                    Insert(string.Format("INSERT INTO DictionaryPositiveNegative (Word,IsPositive,NegativeAppearanceCount,IsEmoticon) VALUES('{0}','{1}',1,'{2}')"
                        , word, isPositive.ToString(), isEmoticon.ToString()));
            }
            else
            {
                //Exists word
                DataRow dr = dt.Rows[0];
                long posCount = long.Parse(dr["PositiveAppearanceCount"].ToString()), negCount = long.Parse(dr["NegativeAppearanceCount"].ToString());
                if (isPositive)
                {
                    isPositive = posCount + 1 > negCount;
                    Update(string.Format("UPDATE DictionaryPositiveNegative SET PositiveAppearanceCount = CAST(PositiveAppearanceCount AS BIGINT) + 1, IsPositive = '{0}' WHERE ID = {1}", isPositive, dr["ID"]));
                }
                else
                {
                    isPositive = posCount > negCount + 1;
                    Update(string.Format("UPDATE DictionaryPositiveNegative SET NegativeAppearanceCount = CAST(NegativeAppearanceCount AS BIGINT) + 1, IsPositive = '{0}' WHERE ID = {1}", isPositive, dr["ID"]));
                }
            }

            // Learn new words using Age API
            Global.AddSentenceToBufferForChecking(word);
        }
        
        #endregion

        #region TweetsPosNeg

        public void SaveTweetPosNegRank(PosNegTweet pnt)
        {
            DataTable dt = Select(string.Format("SELECT ID FROM Tweets WHERE ID = {0} OR RetweetID = {0}", pnt.ID));
            string[] ids = dt.AsEnumerable()
                            .Select(row => row["ID"].ToString())
                            .ToArray();
            Update(string.Format("UPDATE Tweets SET PositiveWords = {0}, NegativeWords = {1}, Rank = {2} WHERE ID IN ({3})", pnt.GetPositive(), pnt.GetNegative(), pnt.LocalRank, string.Join(",", ids)));
        }

        #endregion
       
        #region Tweet Age

        public bool UpdateUserPropertiesByUserID(string columnName, int value, long userID)
        {
            try
            {
                string sqlQuery = string.Format("UPDATE UserProperties SET {0} = {1} WHERE UserID = {2}", columnName, value, userID);
                db.Update(sqlQuery);
                return true;
            }
            catch (Exception e)
            {
                new TwitterException(e);
                return false;
            }

        }
        
        #region Image Analysis

        public void BadUrlProfileImage(long userProfileID)
        {
            try
            {
                Update("UPDATE UserProperties SET ImageAnalysisChecked = 1 WHERE ID = {0}", userProfileID);
            }
            catch (Exception e)
            {
                new TwitterException(e);
            }
        }

        public void UpdateUserProfile(User user)
        {
            string sqlQuery = string.Format("UPDATE Users SET Description = {0}, BackgroundImage = {1}, BannerImage = {2}, ProfileImage = {3} WHERE ID = {4}", ReplaceQuote(user.Description),
                                            ReplaceQuote(user.BackgroundImage), ReplaceQuote(user.BannerImage), ReplaceQuote(user.ProfileImage.Replace("_normal", "")), user.ID);
            Update(sqlQuery);
        }

        public void UpdateUserPropertiesAnalysisByAPI(long userPropertiesID, string gender, double genderConfidence, int lowAge, int highAge, double ageConfidence)
        {
            try
            {
                if(lowAge != 0 && highAge != 0)
                    Update("UPDATE UserProperties SET ImageAnalysisGender = '{0}', ImageAnalysisGenderConfidence = {1}, ImageAnalysisLowAge = {2}, ImageAnalysisHighAge = {3}, ImageAnalysisAgeConfidence = {4}, ImageAnalysisChecked = 1 WHERE ID = {5}", gender, genderConfidence, lowAge, highAge, ageConfidence, userPropertiesID);
                else if(lowAge == 0 && highAge == 0)
                    Update("UPDATE UserProperties SET ImageAnalysisGender = '{0}', ImageAnalysisGenderConfidence = {1}, ImageAnalysisChecked = 1 WHERE ID = {2}", gender, genderConfidence, userPropertiesID);
                else if(lowAge != 0)
                    Update("UPDATE UserProperties SET ImageAnalysisGender = '{0}', ImageAnalysisGenderConfidence = {1}, ImageAnalysisLowAge = {2}, ImageAnalysisAgeConfidence = {3}, ImageAnalysisChecked = 1 WHERE ID = {4}", gender, genderConfidence, lowAge, ageConfidence, userPropertiesID);
                else
                    Update("UPDATE UserProperties SET ImageAnalysisGender = '{0}', ImageAnalysisGenderConfidence = {1}, ImageAnalysisHighAge = {2}, ImageAnalysisAgeConfidence = {3}, ImageAnalysisChecked = 1 WHERE ID = {4}", gender, genderConfidence, highAge, ageConfidence, userPropertiesID);
            }
            catch (Exception e)
            {
                new TwitterException(e);
            }
        }

        #endregion
       
        #endregion

        #region Settings Form

        public void UpdateThreadDesirableState(int processID, string threadState)
        {
            Update("UPDATE ThreadsControl SET ThreadDesirableState = '{0}' WHERE ThreadProcessID = {1} AND MachineName = '{2}'", threadState, processID, Environment.MachineName);
        }

        #endregion

        #region Supervisor

        public void ChangeThreadState(int processID, SupervisorThreadState threadState)
        {
            Update("UPDATE ThreadsControl SET ThreadState = '{0}' WHERE ThreadProcessID = {1} AND MachineName = '{2}'", threadState, processID, Environment.MachineName);
        }

        #endregion

        #endregion

        #region Upsert

        #region Common

        public void UpsertWordToAgeDictionaryAfterUsingAPI(List<WordAge> wordAgeList)
        {
            DataTable dt;
            foreach(WordAge wordAge in wordAgeList)
            {            
                dt = Select("SELECT ID FROM DictionaryAge WHERE Word = '{0}'", wordAge.Word);
                if (dt != null && dt.Rows.Count == 0)
                {
                    // New word
                    Insert(@"INSERT INTO DictionaryAge (Word,Age13To18,Age19To22,Age23To29,Age30Plus,MostPositiveAgeGroup,MostNegativeAgeGroup,LastUpdate) 
                    VALUES ('{0}',{1},{2},{3},{4},{5},{6},'{7}')", wordAge, wordAge.Age13To18, wordAge.Age19To22,
                              wordAge.Age23To29, wordAge.Age30Plus, wordAge.MostPositiveAgeGroup, wordAge.MostNegativeAgeGroup, DateTime.Now.ToString(SqlServerDateTimeFormat));
                }
            }
        }

        #endregion

        #region Supervisor

        public void UpsertThread(string threadName, int subjectID, int processID, SupervisorThreadState threadState = SupervisorThreadState.Stop, SupervisorThreadState threadDesirableState = SupervisorThreadState.Stop)
        {
            DataTable dt = Select("SELECT * FROM ThreadsControl WHERE ThreadName = '{0}' AND SubjectID = {1} AND MachineName = '{2}'", threadName, subjectID, Environment.MachineName);
            if (dt == null || dt.Rows.Count == 0)
            {
                // Insert new record
                Insert("INSERT INTO ThreadsControl (ThreadName,SubjectID,ThreadState,ThreadDesirableState,ThreadProcessID,MachineName) "
                    + "VALUES ('{0}',{1},'{2}','{3}',{4},'{5}')", threadName, subjectID, threadState.ToString(), threadDesirableState.ToString(), processID, Environment.MachineName);
            }
            else
            {
                // Update exists record
                int recordID = int.Parse(dt.Rows[0]["ID"].ToString());
                Update("UPDATE ThreadsControl SET ThreadState = '{0}', ThreadDesirableState = '{1}',ThreadProcessID = {2} WHERE ID = {3}", threadState.ToString(), threadDesirableState.ToString(), processID, recordID);
            }
        }

        public SupervisorThreadState UpsertThreadWithLastStateReturn(string threadName, int subjectID, int processID, SupervisorThreadState threadState = SupervisorThreadState.Stop, SupervisorThreadState threadDesirableState = SupervisorThreadState.Stop)
        {
            DataTable dt = Select("SELECT * FROM ThreadsControl WHERE ThreadName = '{0}' AND SubjectID = {1} AND MachineName = '{2}'", threadName, subjectID, Environment.MachineName);
            if (dt == null || dt.Rows.Count == 0)
            {
                // Insert new record
                Insert("INSERT INTO ThreadsControl (ThreadName,SubjectID,ThreadState,ThreadDesirableState,ThreadProcessID,MachineName) "
                    + "VALUES ('{0}',{1},'{2}','{3}',{4},'{5}')", threadName, subjectID, threadState.ToString(), threadDesirableState.ToString(), processID, Environment.MachineName);
            }
            else
            {
                // Update exists record
                int recordID = int.Parse(dt.Rows[0]["ID"].ToString());
                threadDesirableState = dt.Rows[0]["ThreadDesirableState"].ToString().Equals(SupervisorThreadState.Start.ToString()) ? SupervisorThreadState.Running : SupervisorThreadState.Stop;
                Update("UPDATE ThreadsControl SET ThreadProcessID = {0} WHERE ID = {1}", processID, recordID);
            }
            return threadDesirableState;
        }

        public void UpdateThreadProcessID(string threadName, int subjectID, int processID)
        {
            DataTable dt = Select("SELECT * FROM ThreadsControl WHERE ThreadName = '{0}' AND SubjectID = {1} AND MachineName = '{2}'", threadName, subjectID, Environment.MachineName);
            if (dt == null || dt.Rows.Count == 0)
            {
                
            }
            else
            {
                // Update exists record
                int recordID = int.Parse(dt.Rows[0]["ID"].ToString());                
                Update("UPDATE ThreadsControl SET ThreadProcessID = {0} WHERE ID = {1}", processID, recordID);
            }
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
