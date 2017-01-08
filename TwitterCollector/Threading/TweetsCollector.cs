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
            foreach(KeyValuePair<int,string> keyword in keywords)
            {
                List<Tweet> tweets = GetTweetsByKeyword(keyword.Value);
                foreach (Tweet tweet in tweets)
                {
                    Tweet t = tweet;
                    IsTweetRelevantToSubject(ref t);
                    db.SaveTweet(t);
                }

            }
            db.UpdateSubjectStatus(subjectID, false); //  Set flag to false
            ContinueToSearch();

        }
        private void ContinueToSearch()
        {
            List<Tweet> topTweets = db.GetTopTweets(keywords);
            if (topTweets.Count == 0) ZeroPoint();
            foreach(Tweet t in topTweets)
            {
                List<long> retweetsIDs = twitter.GetRetweetIDs(t.id_str);
                foreach (long id in retweetsIDs)    // For each user that has retweet.
                {
                    User u = twitter.GetUserProfile("", id);
                    if (u == null) continue;
                    List<Tweet> tweets = twitter.GetTweets("", u.id);
                    foreach (Tweet tweet in tweets)
                    {
                        Tweet tmpT = tweet;
                        IsTweetRelevantToSubject(ref tmpT);
                        db.SaveTweet(tmpT);
                    }
                    db.SetSingleValue("Users", "HasAllHistory", id, "True");    //Update this user get his all tweets
                    // TODO: set flag in db sign that all user tweets arrived.

                }
            }
        }

        /// <summary>
        /// There no more tweets in the DB that hasn't checked already.
        /// </summary>
        private void ZeroPoint()
        {
        }
         
        /// <summary>
        /// Get tweet referance and put the subject keyword id, if contains one, else set zero.
        /// </summary>
        /// <param name="tweet">Tweet object.</param>
        /// <returns>True if contains keyword, else false.</returns>
        private bool IsTweetRelevantToSubject(ref Tweet tweet) 
        {
            foreach (KeyValuePair<int,string> key in keywords)
            {
                if (tweet.text.Contains(key.Value))
                {
                    tweet.keywordID = key.Key;
                    return true;
                }
            }
            return false;
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
            List<Tweet> t = t1.GroupBy(x => x.id).Select(x => x.First()).ToList<Tweet>();
            return t;
        }
    }
}
