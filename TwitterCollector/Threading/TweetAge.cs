using Twitter.Classes;
using TwitterCollector.Objects;
using System;
using System.Data;
using System.Linq;
using System.Text;
using TwitterCollector.Common;
using System.Collections.Generic;

namespace TwitterCollector.Threading
{
    class TweetAge : BaseThread
    {

        /// <summary>
        /// Need Get User All Tweets
        /// Create Text that contain all user tweet
        /// ApiAnalysis - send text to api
        /// LocalAnalysis - analyeze each word 
        /// compare result
        /// Update User Score
        /// </summary>

        #region Params

        private AgeTweets ageTweet = new AgeTweets();
        private int[] ageHash =new int[4];
        private string[] emoticonsArraySymbole;
        private string tweetWithoutPunctuation;
        private int[] emoticonsArrayValue;
        private List<Tweet> usertweethistory;
        private List<User> users;
        private int? wordValue;

        #endregion
        public override void RunThread()
            {

            ///get Emoticons from DB to array
            GetEmoticonsFromDB();
            while (ThreadOn)
                {
                    try
                    {
                    users = db.GetUserToCheckSentementAnalysis(ThreadType.TWEET_AGE);

                        if (users.Count == 0)
                        {
                            Global.Sleep(60);
                        }
                        else
                        {
                            foreach (User user in users)
                            {
                                ClearAgeHash();
                                usertweethistory = db.GetUserTweet(user);
                                foreach (Tweet tweet in usertweethistory)
                                {
                                    CompareSentenceEmoticonsArray(tweet.Text);
                                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "");
                                //string[] splitSentence = SplitByDelimiters(tweet.Text, " ");
                                    CompareSentenceToAgeDictionary(tweetWithoutPunctuation);


                                }
                                SaveUserAgeScoreToDB();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        new TwitterException(e);
                    }
                }
            }

        private void SaveUserAgeScoreToDB()
        {
            //Save User Age score to database
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        }

        /// <summary>
        /// This function compare each word from sentence to dictionary Age Value
        /// </summary>
        /// <param name="splitSentence"></param>
        private void CompareSentenceToAgeDictionary(string splitSentence)
        {





            foreach (string word in splitSentence)
            { 
                wordValue = db.GetAgeValueByWord(word);
                if (wordValue.HasValue)
                {
                    UpdateHashAge(wordValue.Value);
                }
            }

        }
        /// <summary>
        /// This function get Emoticons from DB
        /// Update the emoticons array by symbole and value
        /// </summary>
        private void GetEmoticonsFromDB()
        {
            DataTable dt = db.FindEmoticonsForAges();
            emoticonsArraySymbole = new string[dt.Rows.Count];
            emoticonsArrayValue = new int[dt.Rows.Count];
            int index = 0;
            if (dt == null || dt.Rows.Count == 0) return;
            foreach (DataRow dr in dt.Rows)
            {
                //Update HashTable By Normal Values
                emoticonsArraySymbole[index]; //?
                emoticonsArrayValue[index];//?
            }
           

        }

        private void UpdateHashAge(int value)
        {
            //Update HashTable By Normal Values
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        }

        /// <summary>
        /// This function check if sentence conatin emoticons,
        /// and update emoticons array
        /// </summary>
        /// <param name="Sentence"></param>
        private void CompareSentenceEmoticonsArray(string Sentence)
        {
            for (int index=0;index<emoticonsArraySymbole.Length;index++)
            {
                if(Sentence.Contains (emoticonsArraySymbole[index]))
                {
                    UpdateHashAge(emoticonsArrayValue[index]);
                }
            }
        }

        private void ClearAgeHash()
        {
            for (int i= 1; i< ageHash.Length;i++)
            {
                ageHash[i] = 0;
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
    }
}
