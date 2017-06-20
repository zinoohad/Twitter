using System;
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
    public class UserPosNeg : BaseThread
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

        private List<string> _positiveWords, _negativeWords, _positiveEmoticons, _negativeEmoticons;

        #endregion

        public override void RunThread()
        {
            LoadWordLists();    //Save Table content in the RAM
            while (ThreadOn)
            {
                try
                {
                    // Get tweets for user
                    List<long> userIDs = db.GetUsersForFastPosNegAnalysis(true);    // Get random users to check.
                    string tweetsString = "";
                    if (userIDs.Count > 0)
                    {
                        foreach (long userID in userIDs)
                        {
                            tweetsString = "";
                            posNegTweet.Clear();    // Clear the positive and negative object
                            posNegTweet.UserID = _currentUserID = userID;
                            tweets = db.GetTweetsByUserID(userID);
                            if (tweets.Count == 0)
                            {
                                db.AnalyzeUser(posNegTweet);    // Set user rank value to zero
                                db.UnlockUser(userID);
                                continue;
                            }
                            tweetsString = string.Join(" ", tweets.Select(t => t.Text).ToArray());
                            AnalyzeTweet(tweetsString);

                            db.AnalyzeUser(posNegTweet);    //Set user pos neg to current subject into UserProperties Table.
                            db.UnlockUser(userID);
                        }
                    }
                    else
                    {
                        int sleepTime = int.Parse(db.GetValueByKey("UserPosNegSleepInterval", 30).ToString());
                        Global.Sleep(sleepTime);
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

        private void AnalyzeTweet(string tweets)
        {            
            FindEmoticons(tweets);   // Find the emoticons in the text
            string textWithoutPunctuation = Global.GetStringWithoutPunctuation(tweets);    // Remove punctuation from the text
            FindPositiveAndNegativeWords(textWithoutPunctuation);   // Check for positive and negative words
            posNegTweet.CalculateRank();    // Calculate the total score for the current tweet
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
            posNegTweet.PositiveEmoticons = _positiveEmoticons.FindRepeats(splitSentence);
            posNegTweet.NegativeEmoticons = _negativeWords.FindRepeats(splitSentence);
        }

        private void FindPositiveAndNegativeWords(string sentence)
        {
            sentence = sentence.Replace("'", "''");
            List<string> splitSentence = Global.SplitSentenceToSubSentences(sentence, int.Parse(db.GetValueByKey("MaxWordInSubSentence", 3).ToString()));
            posNegTweet.PositiveWords = _positiveWords.FindRepeats(splitSentence.ToArray());
            posNegTweet.NegativeWords = _negativeWords.FindRepeats(splitSentence.ToArray());
        }

        private void LoadWordLists()
        {
            // Get Emoticons
            DataTable dt = db.GetTable("DictionaryPositiveNegative", "IsEmoticon = 'True'");
            _positiveEmoticons = dt.AsEnumerable().Where(r => r.Field<bool>("IsPositive") == true).Select(r => r.Field<string>("Word")).ToList();
            _negativeEmoticons = dt.AsEnumerable().Where(r => r.Field<bool>("IsPositive") == false).Select(r => r.Field<string>("Word")).ToList();

            // Get Positive Words
            dt = db.GetTable("DictionaryPositiveNegative", "IsPositive = 'True' AND IsEmoticon = 'False'");
            _positiveWords = dt.AsEnumerable().Select(r => r.Field<string>("Word")).ToList();

            // Get Negative Words
            dt = db.GetTable("DictionaryPositiveNegative", "IsPositive = 'False' AND IsEmoticon = 'False'");
            _negativeWords = dt.AsEnumerable().Select(r => r.Field<string>("Word")).ToList();
        }
    }
}
