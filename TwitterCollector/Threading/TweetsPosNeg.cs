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

        #endregion

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    tweets = db.GetTweetsToCheckSentementAnalysis(ThreadType.TWEET_POS_NEG);

                    if (tweets.Count == 0)
                    {
                        Global.Sleep(60);
                    }
                    else
                    {
                        foreach (Tweet t in tweets)
                        {
                            posNegTweet.Clear();    // Clear the positive and negative object
                            posNegTweet.ID = t.id_str;
                            FindEmoticons(t.Text);   // Find the emoticons in the text
                            string textWithoutPunctuation = GetStringWithoutPunctuation(t.Text);    // Remove punctuation from the text
                            FindPositiveAndNegativeWords(textWithoutPunctuation);   // Check for positive and negative words
                            posNegTweet.CalculateRank();    // Calculate the total score for the current tweet
                            db.SaveTweetPosNegRank(posNegTweet);

                        }
                    }
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }   
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

        private string GetStringWithoutPunctuation(string oldString)
        {
            var newString = new StringBuilder();
            for (int i = 0 ; i < oldString.Length ; i++)
            {
                char c = oldString[i];
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
                    || (i != 0 && i != oldString.Length - 1 && char.IsLetter(oldString[i-1]) && char.IsLetter(oldString[i+1]) && c.In('-','\'')))
                    newString.Append(c);
            }
            return newString.ToString();
        }

        private void FindPositiveAndNegativeWords(string sentence)
        {
            sentence = sentence.Replace("'", "''");
            string[] splitSentence = SplitByDelimiters(sentence, " ");
            DataTable dt = db.FindPositiveNegativeWords(splitSentence);

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
