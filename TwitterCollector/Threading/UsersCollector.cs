﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twitter;
using Twitter.Classes;
using Twitter.Interface;
using TwitterCollector.Common;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    public class UsersCollector : BaseThread, Update
    {
        #region Params

        private List<KeywordO> keywords = new List<KeywordO>();

        private List<Tweet> GlobalTweetsList = new List<Tweet>();

        private static readonly object locker = new object();

        private int subjectID;

        private string _languageCode;

        private long _currentUserID = -1;

        #endregion

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    db.UnlockAllUsers();
                    keywords = db.GetSubjectKeywordsList(subjectID);
                    
                    // Run stream thread
                    if (streamThread == null || !streamThread.IsAlive) (streamThread = new Thread(new ThreadStart(this.ManageOnStream))).Start();
                    StartToCollect();

                }
                catch (Exception e)
                {
                    new TwitterException(e);
                    if (_currentUserID != -1)
                        db.UnlockUser(_currentUserID);
                }
            }
        }

        private void StartToCollect()
        {
            List<long> usersID = db.GetTopUncheckedUsers();
            foreach (long id in usersID)
            {
                //_currentUserID = id;
                // The API update this class in real time for every request. 
                // That the reason why we don't need to do nothing.
                List<Tweet> userTweets = twitter.GetTweets("", id,3200,200,this);
                //TODO: UPDATE DATABASE THIS USER HAS ALL HIS TWEETS
                if(userTweets.Count > 0 )
                    db.SetSingleValue("Users", "HasAllHistory", id, "'True'");
                //db.UnlockUser(_currentUserID);
            }
        }

        private void ManageOnStream()
        {
            while (ThreadOn)
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
                    if (!e.Message.StartsWith("Violation of PRIMARY KEY"))
                        new TwitterException(e); 
                }
            }
        }

        public override void SetInitialParams(params object[] Params)
        {
            subjectID = (int)Params[0];
            _languageCode = Params[1].ToString();
        }

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
                if (!IsTweetRelevantToSubject(ref tweetPointer, ref sameLanguage)) //Add all keywords id that relevant to subject in the tweet text
                {
                    if (!sameLanguage || !tweet.Language.Equals(_languageCode)) //The tweet not in the same language as the keyword or subject
                    {
                        continue;
                    }
                }
                db.SaveTweet(tweetPointer);
            }
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

        #region Synchronized Functions

        public void AddSafeData(List<Tweet> tweets)
        {
            lock (locker)
            {
                GlobalTweetsList.AddRange(tweets);
            }
        }
        private List<Tweet> GetSafeData()
        {
            List<Tweet> tweets;
            lock (locker)
            {
                tweets = GlobalTweetsList;
                GlobalTweetsList = new List<Tweet>();
            }
            return tweets;
        }
        #endregion
    }
}
