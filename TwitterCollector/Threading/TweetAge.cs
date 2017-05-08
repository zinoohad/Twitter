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

        public const int AGE_GROUP_13_TO_18 = 1;

        public const int AGE_GROUP_19_TO_22 = 2;

        public const int AGE_GROUP_23_TO_29 = 3;

        public const int AGE_GROUP_30_PLUS = 4;

        private List<double> ageHash = Enumerable.Repeat<double>(0, 4).ToList();

        private int maxWordInSubSentence;
        private List<WordAge> emoticonsArrayValue;
        private List<Tweet> userTweetHistory;
        private List<long> usersIDs;
        private string tweetWithoutPunctuation;
        private WordClassification TestDictionary;                                        //WTF! (Name & Capital letter)

        #endregion
        public override void RunThread()
        {
            emoticonsArrayValue = db.FindEmoticonsForAges();
            maxWordInSubSentence = int.Parse(db.GetValueByKey("MaxWordInSubSentence",3).ToString());
            while (ThreadOn)
            {
                try
                {
                    usersIDs = db.GetUserIDsToCheckAnalysisByAge(ThreadType.TWEET_AGE);                       //WTF! (In this function their is no check on the send argument, it's not relevant).

                    if (usersIDs.Count == 0)
                    {
                        Global.Sleep(60);
                    }
                    else
                    {
                        foreach (long userID in usersIDs)
                        {
                            ClearAgeHash();
                            userTweetHistory = db.GetUserTweetByUserID(userID);
                            TestDictionary = new WordClassification();
                            foreach (Tweet tweet in userTweetHistory)
                            {
                                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "").Replace("@", "");//.Replace("RT", "");    //WTF! (All the replase, need to understand way to use them)
                                CompareSentenceEmoticonsArray(tweetWithoutPunctuation);

                               
                                // breack the sentence to combinations of maxWordInSubSentence and compare to db dictionary
                                if (!string.IsNullOrEmpty(tweetWithoutPunctuation))                                                                              //WTF! (Added empty string check!)
                                    CompareSentenceToAgeDictionary(Global.SplitSentenceToSubSentences(tweetWithoutPunctuation, maxWordInSubSentence));
                                //ageHash.ForEach(item => Console.Write(+item + ","));
                                //Console.WriteLine();
                            }
                        /*Test Dictionary*/
                        //TestDictionary.print();

                        SaveUserAgeScoreToDB(userID);
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
            db.UpdateUserPropertiesByUserID( "AgeGroupID", ageHash.IndexOf(max) + 1, userID);   
        }

        /// <summary>
        /// This function compare each word or words combination from
        /// the original sentence to dictionary Age Value
        /// </summary>
        /// <param name="splitSentence"></param>
        private void CompareSentenceToAgeDictionary(List<string> splitSentences)
        {
            int index = 0;                                        //WTF!
            foreach (string word in splitSentences)
            {
                index++;                                        //WTF!
                WordAge returnValue = null;
                try
                {
                    returnValue = db.GetAgeValueByWord(word.Replace("'","''"));
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
                if (returnValue != null) //need to check empty  
                {
                    switch (returnValue.MostPositiveAgeGroup)                                         //WTF! (Same code lines in line 159. Create common function.)
                    {
                        case AGE_GROUP_13_TO_18:                           
                            ageHash[0] += returnValue.Age13To18;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age13To18, AGE_GROUP_13_TO_18);
                            break;
                        case AGE_GROUP_19_TO_22:
                            ageHash[1] += returnValue.Age19To22;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age19To22, AGE_GROUP_19_TO_22);
                            break;
                        case AGE_GROUP_23_TO_29:
                            ageHash[2] += returnValue.Age23To29;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age23To29, AGE_GROUP_23_TO_29);
                            break;
                        case AGE_GROUP_30_PLUS:
                            ageHash[3] += returnValue.Age30Plus;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age30Plus, AGE_GROUP_30_PLUS);
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
                    switch (Emoticon.MostPositiveAgeGroup)                                          //WTF! (Same code lines in line 123. Create common function.)         
                    {
                        case AGE_GROUP_13_TO_18:
                            ageHash[0] += Emoticon.Age13To18;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age13To18, AGE_GROUP_13_TO_18);
                            break;
                        case AGE_GROUP_19_TO_22:
                            ageHash[1] += Emoticon.Age19To22;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age19To22, AGE_GROUP_19_TO_22);
                            break;
                        case AGE_GROUP_23_TO_29:
                            ageHash[2] += Emoticon.Age23To29;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age23To29, AGE_GROUP_23_TO_29);
                            break;
                        case AGE_GROUP_30_PLUS:
                            ageHash[3] += Emoticon.Age30Plus;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age30Plus, AGE_GROUP_30_PLUS);
                            break;
                    }//switch
                }//if
            }//foreach
        }

        private void ClearAgeHash()
        {
            for (int i = 1 ; i < ageHash.Count ; i++)
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
        private string[] SplitByDelimiters(string str, params string[] delimiterStrings)                                             //WTF! (Not in Use.)
        {
            return str.Split(delimiterStrings, StringSplitOptions.RemoveEmptyEntries);
        }

        private string GetStringWithoutPunctuation(string oldString)                                         //WTF! (Why not use the function from global class? This function copied from Global.)
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
