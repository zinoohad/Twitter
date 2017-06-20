using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using Twitter;
using Twitter.Classes;
using Twitter.Interface;
using System.Threading;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    public class TweetsCollector : BaseThread, Update
    {
        #region Params

        private int _subjectID;

        private bool _newSubject;

        private string _languageCode;

        private List<KeywordO> keywords = new List<KeywordO>();
        
        private List<Tweet> globalTweetList = new List<Tweet>();

        private static readonly object locker = new object();

        #endregion

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    // Run stream thread
                    if (streamThread == null || !streamThread.IsAlive) (streamThread = new Thread(new ThreadStart(this.ManageOnStream))).Start();

                    //if (_newSubject) StartNewSearch();
                    StartNewSearch();
                    //if (true) StartNewSearch();
                    //else ContinueToSearch();
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }
        }

        public override void SetInitialParams(params object[] Params)
        {
            _subjectID = (int)Params[0];
            _newSubject = (bool)Params[1];
            _languageCode = Params[2].ToString();
            keywords = db.GetSubjectKeywordsList(_subjectID);
        }

        #region Collector Main Functions
        /// <summary>
        /// New search need to start.
        /// It's possible there a new subjects to check.
        /// </summary>
        private void StartNewSearch()
        {
            foreach (KeywordO keyword in keywords)
            {
                List<Tweet> tweets = GetTweetsByKeyword(keyword.Name, keyword.ID);
            }   
            db.UpdateSubjectStatus(_subjectID, false); //  Set flag to false
            ContinueToSearch();

        }

        private void ContinueToSearch()
        {
            while (ThreadOn)
            {
                List<Tweet> topTweets = db.GetTopTweets(_subjectID);
                if (topTweets.Count == 0) ZeroPoint();
                else ExploreTweets(topTweets);
            }
        }

        /// <summary>
        /// There no more tweets in the DB that hasn't checked already.
        /// </summary>
        private void ZeroPoint()
        {
            List<long> topRatedUsersIDs = db.GetTopUsersIDRatingForZeroPoint(_subjectID);    // Get top (X) rated users
            List<Tweet> tweets = db.TopRatedNotRelatedSubjectTweet(_subjectID, topRatedUsersIDs.ToArray());  //Get users top rated, but not related to subject, tweet.
            ExploreTweets(tweets);
        }

        /// <summary>
        /// Collect all relevant information about given tweet
        /// </summary>
        /// <param name="tweets">List of all the tweets need to check.</param>
        private void ExploreTweets(List<Tweet> tweets)
        {
            foreach (Tweet t in tweets)
            {
                List<long> retweetsIDs = twitter.GetRetweetIDs(t.id_str);
                foreach (long id in retweetsIDs)    // For each user that has retweet.
                {
                    User u = twitter.GetUserProfile("", id);
                    if (u == null || (db.GetSingleValue("Users", "HasAllHistory", string.Format("ID = {0} AND HasAllHistory = 1", id))) != null) continue;
                    db.InsertUser(u);
                    List<Tweet> userTweets = twitter.GetTweets("", u.ID, 3200, 200, this);
                    //CheckSubjectRelevantTweetAndSave(userTweets);
                    db.SetSingleValue("Users", "HasAllHistory", id, "'True'");    //  Update this user get his all tweets
                }
                db.SetSingleValue("Tweets", "CheckedByTweetCollector", t.ID, 1);    // Tweet was checked
            }
        }

        private void ManageOnStream()
        {
            while(ThreadOn)
            {
                try
                {
                    List<Tweet> tweets;
                    while (ThreadOn)
                    {
                        tweets = GetSafeData();     // Get the data without create collision with another thread
                        if (tweets.Count == 0) Global.Sleep(10);
                        else
                            CheckSubjectRelevantTweetAndSave(tweets);
                    }
                }
                catch (Exception e) 
                { 
                    if(!e.Message.StartsWith("Violation of PRIMARY KEY"))
                        new TwitterException(e); 
                }
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Get tweet referance and put the subject keyword id, if contains one, else set zero.
        /// </summary>
        /// <param name="tweet">Tweet object.</param>
        /// <returns>True if contains keyword, else false.</returns>
        private bool IsTweetRelevantToSubject(ref Tweet tweet, ref bool sameLanguage)
        {
            sameLanguage = true;
            List<int> keyword = new List<int>();
            foreach (KeywordO key in keywords)
            {
                if (tweet.Text.ToLower().Contains(key.Name.ToLower()))
                {
                    if (!tweet.Language.Equals(key.LanguageCode))
                    {
                        sameLanguage = false;
                        return false;
                    }
                    keyword.Add(key.ID);
                }
            }           
            tweet.keywordID = keyword;
            if (keyword.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// Check if tweets relevant to subject and save them in DB.
        /// The tweets must contains all the parameters as them pulled from Twitter API.
        /// </summary>
        /// <param name="tweets">List of tweets to check.</param>
        private void CheckSubjectRelevantTweetAndSave(List<Tweet> tweets)
        {
            bool sameLanguage = true;
            foreach (Tweet tweet in tweets)
            {
                
                Tweet tweetPointer = tweet;
                if (IsTweetRelevantToSubject(ref tweetPointer, ref sameLanguage)) //Add all keywords id that relevant to subject in the tweet text
                {
                    //TODO: Check if it's work. (Added: 1/4/17)
                    KeywordO k = (from key in keywords where key.ID == tweetPointer.keywordID[0] select key).SingleOrDefault();
                    if (tweetPointer.Language != k.LanguageCode) continue;  // This is not language that we need
                }
                else if (!sameLanguage || !tweet.Language.Equals(_languageCode)) //The tweet not in the same language as the keyword or subject
                {
                    continue;
                }

                db.SaveTweet(tweetPointer);               
            }
        }

        /// <summary>
        /// Return tweets searched by keyword, distinct by tweet id.
        /// </summary>
        /// <param name="keyword">String to search</param>
        /// <returns>List of all relevant tweets</returns>
        private List<Tweet> GetTweetsByKeyword(string keyword, int keywordID)
        {
            string lang = db.GetLanguageCodeFromKeyword(keywordID);
            List<Tweet> t1 = twitter.SearchTweets(keyword, 50000, "recent", 100, lang, null, true, this);
            List<Tweet> t2 = twitter.SearchTweets(keyword, 10000, "popular", 100, lang, null, true, this);
            t1.AddRange(t2);
            List<Tweet> t = t1.GroupBy(x => x.ID).Select(x => x.First()).ToList<Tweet>();
            return t;
        }
       
        #endregion

        #region Synchronized Functions
        public void AddSafeData(List<Tweet> tweets)
        {
            lock (locker)
            {
                globalTweetList.AddRange(tweets);
            }
        }
        private List<Tweet> GetSafeData()
        {
            List<Tweet> tweets;
            lock (locker)
            {
                tweets = globalTweetList;
                globalTweetList = new List<Tweet>();
            }
            return tweets;
        }
        #endregion

        #region Implement Methods
        /// <summary>
        /// Data update with paging
        /// </summary>
        /// <param name="obj">Returns object from API</param>
        /// <param name="action">Which function in the API call this method.</param>
        public void Update(object obj, ApiAction action = ApiAction.SEARCH_TWEETS)
        {
            switch (action)
            {
                case ApiAction.SEARCH_TWEETS:
                case ApiAction.GET_TWEETS:
                    AddSafeData((List<Tweet>)obj);
                    break;
            }
        }
        public void EndRequest()
        {

        }
        public override void Abort()
        {
            base.Abort();
            streamThread.Abort();
        }
        #endregion
    }
}
