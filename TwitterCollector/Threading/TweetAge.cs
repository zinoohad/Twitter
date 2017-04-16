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

        private List<double> ageHash = Enumerable.Repeat<double>(0, 4).ToList();

        private int maxWordInSubSentence;
        private List<WordAge> emoticonsArrayValue;
        private List<Tweet> usertweethistory;
        private List<long> usersIDs;
        private string tweetWithoutPunctuation;
        private WordClassification TestDictionary;

        #endregion
        public override void RunThread()
            {

            emoticonsArrayValue = db.FindEmoticonsForAges();
            maxWordInSubSentence= int.Parse(db.GetValueByKey("MaxWordInSubSentence",3).ToString());
            while (ThreadOn)
                {
                    try
                    {
                    usersIDs = db.GetUserIDsToCheckAnalysisByAge(ThreadType.TWEET_AGE);

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
                                TestDictionary = new WordClassification();
                                foreach (Tweet tweet in usertweethistory)
                            {
                                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "").Replace("RT","").Replace("@","");
                                CompareSentenceEmoticonsArray(tweetWithoutPunctuation);

                               
                                // breack the sentence to combinations of maxWordInSubSentence and compare to db dictionary
                             
                                CompareSentenceToAgeDictionary(Global.SplitSentenceToSubSentences(tweetWithoutPunctuation, maxWordInSubSentence));
                                ageHash.ForEach(item => Console.Write(+item + ","));
                                Console.WriteLine();
                               }
                            /*Test Dictionary*/
                            TestDictionary.print();

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
            db.UpdateUserPropertiesByUserID( "AgeGroupID", ageHash.IndexOf(max) + 1, userID);

           
        }

        /// <summary>
        /// This function compare each word or words combination from
        /// the original sentence to dictionary Age Value
        /// </summary>
        /// <param name="splitSentence"></param>
        private void CompareSentenceToAgeDictionary(List<string> splitSentences)
        {
            int index = 0;
            foreach (string word in splitSentences)
            {
                index++;
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
                    switch (returnValue.MostPositiveAgeGroup)
                    {
                        case 1:                           
                            ageHash[0] += returnValue.Age13To18;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age13To18,1);
                            break;
                        case 2:
                            ageHash[1] += returnValue.Age19To22;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age19To22,2);
                            break;
                        case 3:
                            ageHash[2] += returnValue.Age23To29;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age23To29,3);
                            break;
                        case 4:
                            ageHash[3] += returnValue.Age30Plus;
                            TestDictionary.IncreaseWord(returnValue.Word, returnValue.Age30Plus,4);
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
                    
                    switch (Emoticon.MostPositiveAgeGroup)
                    {
                        case 1:
                            ageHash[0] += Emoticon.Age13To18;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age13To18,1);
                            break;
                        case 2:
                            ageHash[1] += Emoticon.Age19To22;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age19To22,2);
                            break;
                        case 3:
                            ageHash[2] += Emoticon.Age23To29;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age23To29,3);
                            break;
                        case 4:
                            ageHash[3] += Emoticon.Age30Plus;
                            TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.Age30Plus,4);
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
