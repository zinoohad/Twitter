using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using Twitter;
using Twitter.Classes;

namespace TwitterCollector.Threading
{
    public class TweetsCollector : BaseThread
    {
        #region Params
        private Dictionary<int, string> keywords = new Dictionary<int, string>();
        private int subjectID;
        private bool newSubject;
        #endregion
        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    keywords = db.GetSubjectKeywords(subjectID);
                    if (newSubject) StartNewSearch();
                    else ContinueToSearch();
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }
        }
        public override void SetInitialParams(params object[] Params)
        {
            subjectID = (int)Params[0];
            newSubject = (bool)Params[1];
        }
        /// <summary>
        /// New search need to start.
        /// It's possible there a new subjects to check.
        /// </summary>
        private void StartNewSearch()
        {
            foreach (KeyValuePair<int, string> keyword in keywords)
            {
                List<Tweet> tweets = GetTweetsByKeyword(keyword.Value);
                CheckSubjectRelevantTweetAndSave(tweets);
            }
            db.UpdateSubjectStatus(subjectID, false); //  Set flag to false
            ContinueToSearch();

        }
        private void ContinueToSearch()
        {
            while (ThreadOn)
            {
                List<Tweet> topTweets = db.GetTopTweets(subjectID);
                if (topTweets.Count == 0) ZeroPoint();
                else ExploreTweets(topTweets);                
            }
        }
        /// <summary>
        /// There no more tweets in the DB that hasn't checked already.
        /// </summary>
        private void ZeroPoint()
        {
            List<long> topRatedUsersIDs = db.GetTopUsersIDRatingForZeroPoint(subjectID);    // Get top (X) rated users
            List<Tweet> tweets = db.TopRatedNotRelatedSubjectTweet(subjectID, topRatedUsersIDs.ToArray());  //Get users top rated, but not related to subject, tweet.
            ExploreTweets(tweets);           
        }         
        /// <summary>
        /// Get tweet referance and put the subject keyword id, if contains one, else set zero.
        /// </summary>
        /// <param name="tweet">Tweet object.</param>
        /// <returns>True if contains keyword, else false.</returns>
        private bool IsTweetRelevantToSubject(ref Tweet tweet) 
        {
            List<int> keyword = new List<int>();
            foreach (KeyValuePair<int,string> key in keywords)
            {
                if (tweet.Text.Contains(key.Value))
                {
                    keyword.Add(key.Key);                 
                }
            }
            tweet.keywordID = keyword;
            if (keyword.Count == 0) return false;
            return true;
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
                    List<Tweet> userTweets = twitter.GetTweets("", u.ID);
                    CheckSubjectRelevantTweetAndSave(userTweets);                   
                    db.SetSingleValue("Users", "HasAllHistory", id, "'True'");    //  Update this user get his all tweets
                }
                db.SetSingleValue("Tweets", "CheckedByTweetCollector", t.ID, 1);    // Tweet was checked
            }
        }
        /// <summary>
        /// Check if tweets relevant to subject and save them in DB.
        /// The tweets must contains all the parameters as them pulled from Twitter API.
        /// </summary>
        /// <param name="tweets">List of tweets to check.</param>
        private void CheckSubjectRelevantTweetAndSave(List<Tweet> tweets)
        {
            foreach (Tweet tweet in tweets)
            {
                Tweet tweetPointer = tweet;
                IsTweetRelevantToSubject(ref tweetPointer); //Add all keywords id that relevant to subject in the tweet text
                db.SaveTweet(tweetPointer);
            }
        }
        /// <summary>
        /// Return tweets searched by keyword, distinct by tweet id.
        /// </summary>
        /// <param name="keyword">String to search</param>
        /// <returns>List of all relevant tweets</returns>
        private List<Tweet> GetTweetsByKeyword(string keyword)
        {
            List<Tweet> t1 = twitter.SearchTweets(keyword, 10000, "recent");
            List<Tweet> t2 = twitter.SearchTweets(keyword, 10000, "popular");
            t1.AddRange(t2);
            List<Tweet> t = t1.GroupBy(x => x.ID).Select(x => x.First()).ToList<Tweet>();
            return t;
        }
    }
}
