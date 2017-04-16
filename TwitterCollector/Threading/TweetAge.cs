using Twitter.Classes;
using TwitterCollector.Objects;
using System;
using System.Data;
using System.Linq;
using System.Text;
using TwitterCollector.Common;
using System.Collections.Generic;
using TopicSentimentAnalysis.Classes;

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

        private List<double> ageHash = new List<double>(4);
        private int maxWordInSubSentence;
        private List<WordAge> emoticonsArrayValue;
        private List<Tweet> usertweethistory;
        private List<long> usersIDs;
        private string tweetWithoutPunctuation;

        #endregion
        public override void RunThread()
            {

            emoticonsArrayValue = db.FindEmoticonsForAges();
            maxWordInSubSentence= int.Parse(db.GetValueByKey("MaxWordInSubSentence",3).ToString());
            while (ThreadOn)
                {
                    try
                    {
                    usersIDs = db.GetUserIDToCheckAnalysisByAge(ThreadType.TWEET_AGE);

                        if (usersIDs.Count == 0)
                        {
                            Global.Sleep(60);
                        }
                        else
                        {
                            foreach (long userID in usersIDs)
                            {
                                ClearAgeHash();
                                usertweethistory = db.GetUserTweetByUserID(userID);
                                foreach (Tweet tweet in usertweethistory)
                                {
                                CompareSentenceEmoticonsArray(tweet.Text);

                                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "");
                                // breack the sentence to combinations of maxWordInSubSentence and compare to db dictionary
                                CompareSentenceToAgeDictionary(Global.SplitSentenceToSubSentences(tweetWithoutPunctuation, maxWordInSubSentence));
                                

                                }
                                SaveUserAgeScoreToDB( userID);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        new TwitterException(e);
                    }
                }
            }

        private void SaveUserAgeScoreToDB(long userID)
        {
            double max = ageHash.Max();
            //find max value
            ageHash.IndexOf(max);
            db.updateUserPropertiesByUserID( "AgeGroupID", ageHash.IndexOf(max) + 1, userID);

            //Save User Age score to database
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        }

        /// <summary>
        /// This function compare each word or words combination from
        /// the original sentence to dictionary Age Value
        /// </summary>
        /// <param name="splitSentence"></param>
        private void CompareSentenceToAgeDictionary(List<string> splitSentences)
        {
            foreach (string word in splitSentences)
            {
                WordAge returnValue = null;
                returnValue = db.GetAgeValueByWord(word);
                if (returnValue.Word != null) //need to check empty  
                {
                    switch (returnValue.MostPositiveAgeGroup)
                    {
                        case 1:
                            ageHash[0] += returnValue.Age13To18;
                            break;
                        case 2:
                            ageHash[1] += returnValue.Age19To22;
                            break;
                        case 3:
                            ageHash[2] += returnValue.Age23To29;
                            break;
                        case 4:
                            ageHash[3] += returnValue.Age30Plus;
                            break;
                    }//switch
                }//if
            }//foreach
        }
  


        /// <summary>
        /// This function check if sentence conatin emoticons,
        /// and update emoticons array
        /// </summary>
        /// <param name="Sentence"></param>
        private void CompareSentenceEmoticonsArray(string Sentence)
        {
            foreach (WordAge Emoticon in emoticonsArrayValue)
            {
                if(Sentence.Contains( Emoticon.Word) )
                {
                    switch(Emoticon.MostPositiveAgeGroup)
                    {
                        case 1:
                            ageHash[0] += Emoticon.Age13To18;
                            break;
                        case 2:
                            ageHash[1] += Emoticon.Age19To22;
                            break;
                        case 3:
                            ageHash[2] += Emoticon.Age23To29;
                            break;
                        case 4:
                            ageHash[3] += Emoticon.Age30Plus;
                            break;
                    }//switch
                }//if
            }//foreach
        }

        private void ClearAgeHash()
        {
            for (int i= 1; i< ageHash.Count;i++)
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
