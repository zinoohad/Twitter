﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TopicSentimentAnalysis;
using TopicSentimentAnalysis.Classes;
using Twitter.Classes;
using TwitterCollector.Common;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    public class TweetsPosNeg : BaseThread
    {
        /// <summary>
        /// Need to read tweet
        /// split it to words or sub sentence
        /// check first if has emoji 14420-14451
        /// remove all punctuation from sentence
        /// check in loop if the words in the dectionary
        /// set pos / neg score
        /// send the sentence to api and get result
        /// learn new words from the api
        /// update all retweets
        /// </summary>
        /// 

        #region Params

        private PosNegTweet posNegTweet = new PosNegTweet();

        private List<Tweet> tweets;

        private long _currentUserID = -1;

        #endregion

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    // Get tweets for user
                    List<long> userIDs = db.GetUsersForPosNegAnalysis(true);    // Get random users to check.
                    if (userIDs.Count > 0)
                    {
                        foreach (long userID in userIDs)
                        {
                            _currentUserID = userID;
                            tweets = db.GetTweetsByUserID(userID);
                            foreach (Tweet t in tweets)
                            {
                                AnalyzeTweet(t);
                            }
                            db.AnalyzeUser(userID);    //Set user pos neg to current subject into UserProperties Table.
                            db.UnlockUser(userID);
                            db.SetSingleValue("Users", "AlreadyChecked", userID, 1);
                        }
                    }
                    else
                    {
                        // Get random tweets
                        tweets = db.GetTweetsToCheckSentementAnalysis(ThreadType.TWEET_POS_NEG);

                        if (tweets.Count == 0)
                        {
                            Global.Sleep(60);
                        }
                        else
                        {
                            foreach (Tweet t in tweets)
                            {
                                AnalyzeTweet(t);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (_currentUserID != -1)
                    {
                        db.UnlockUser(_currentUserID);
                    }
                    new TwitterException(e);
                }
            }   
        }

        //public override void RunThread()
        //{
        //    while (ThreadOn)
        //    {
        //        try
        //        {
        //            tweets = db.GetTweetsToCheckSentementAnalysis(ThreadType.TWEET_POS_NEG);

        //            if (tweets.Count == 0)
        //            {
        //                Global.Sleep(60);
        //            }
        //            else
        //            {
        //                foreach (Tweet t in tweets)
        //                {
        //                    posNegTweet.Clear();    // Clear the positive and negative object
        //                    posNegTweet.ID = t.id_str;
        //                    FindEmoticons(t.Text);   // Find the emoticons in the text
        //                    string textWithoutPunctuation = Global.GetStringWithoutPunctuation(t.Text);    // Remove punctuation from the text
        //                    FindPositiveAndNegativeWords(textWithoutPunctuation);   // Check for positive and negative words
        //                    posNegTweet.CalculateRank();    // Calculate the total score for the current tweet
        //                    db.SaveTweetPosNegRank(posNegTweet);

        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            new TwitterException(e);
        //        }
        //    }
        //}

        private void AnalyzeTweet(Tweet tweet)
        {
            posNegTweet.Clear();    // Clear the positive and negative object
            posNegTweet.ID = tweet.id_str;
            FindEmoticons(tweet.Text);   // Find the emoticons in the text
            string textWithoutPunctuation = Global.GetStringWithoutPunctuation(tweet.Text);    // Remove punctuation from the text
            FindPositiveAndNegativeWords(textWithoutPunctuation);   // Check for positive and negative words
            posNegTweet.CalculateRank();    // Calculate the total score for the current tweet
            db.SaveTweetPosNegRank(posNegTweet);
        }

        /// <summary>
        /// Split given sentence by delimiters
        /// </summary>
        /// <param name="str">Given sentence</param>
        /// <param name="delimiterStrings">Given delimiters</param>
        /// <returns>Return split sentence by delimiters</returns>
        private string[] SplitByDelimiters(string str, params string[] delimiterStrings)
        {
            return str.Split(delimiterStrings, StringSplitOptions.RemoveEmptyEntries);
        }

        private void FindEmoticons(string sentence)
        {
            sentence = sentence.Replace("'", "''").Replace("http://", "").Replace("https://", "");
            string[] splitSentence = SplitByDelimiters(sentence, " ").Where(s => s.Length > 1).ToArray();
            DataTable dt = db.FindEmoticons(splitSentence);
            if(dt == null || dt.Rows.Count == 0) return;
            foreach (DataRow dr in dt.Rows)
            {
                if (bool.Parse(dr["IsPositive"].ToString()) == true)
                    posNegTweet.PositiveEmoticons++;
                else
                    posNegTweet.NegativeEmoticons++;
            }
        }

        private void FindPositiveAndNegativeWords(string sentence)
        {
            sentence = sentence.Replace("'", "''");
            //string[] splitSentence = SplitByDelimiters(sentence, " ");
            List<string> splitSentence = Global.SplitSentenceToSubSentences(sentence, int.Parse(db.GetValueByKey("MaxWordInSubSentence",3).ToString()));
            DataTable dt = db.FindPositiveNegativeWords(splitSentence.ToArray());

            if (dt == null || dt.Rows.Count == 0) return;
            foreach (DataRow dr in dt.Rows)
            {
                if (bool.Parse(dr["IsPositive"].ToString()) == true)
                    posNegTweet.PositiveWords++;
                else
                    posNegTweet.NegativeWords++;
            }
        }
    }
}
