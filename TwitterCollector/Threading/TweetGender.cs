using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopicSentimentAnalysis.Classes;
using Twitter.Classes;
using TwitterCollector.Common;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    class TweetGender:BaseThread
    {
        /// <summary>
        /// Need Get User All Tweets for analsis
        /// Get all User Tweets
        /// Emoticons compare 
        /// word Dictionary comare 
        /// Update User Score
        /// </summary>

        #region Params

        private double userWordValueCounter;

        private int maxWordInSubSentence;
        private List<WordGender> emoticonsArrayValue;
        private List<Tweet> usertweethistory;
        private List<long> usersIDs;
        private string tweetWithoutPunctuation;
        private WordClassification TestDictionary;
        private List<string> senctence;
        private StringBuilder UserText;
        private Dictionary<string, double> GenderDictionary ;
        #endregion
        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    usersIDs = db.GetUserIDsToCheckAnalysisByGender(ThreadType.TWEET_GENDER);

                    if (usersIDs.Count == 0)
                    {
                        Global.Sleep(60);
                    }
                    else
                    {
                        foreach (long userID in usersIDs)
                        {
                            UserText = new StringBuilder();
                            userWordValueCounter = 0;
                            usertweethistory = db.GetUserTweetByUserID(userID);
                            foreach (Tweet tweet in usertweethistory)
                            {
                                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "").Replace("RT", "").Replace("@", "");
                                UserText.Append(tweetWithoutPunctuation + " ");
                            }
                            foreach (KeyValuePair<string, double> entry in GenderDictionary)
                            {
                                int count = 0;
                                count = SplitByDelimiters(UserText.ToString(), entry.Key).Length - 1;
                                if(count > 0 )
                                {
                                    userWordValueCounter += count * entry.Value;
                                   
                                }
                            }
                            SaveUserGenderScoreToDB(userID);
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }
        }

        public TweetGender() : base()
        {
            emoticonsArrayValue = db.FindEmoticonsForGender();
            maxWordInSubSentence = int.Parse(db.GetValueByKey("MaxWordInSubSentence", 3).ToString());
            GenderDictionary = db.GetGenderDictionary();
        }

        private void AnalysisProcessNumberOne(long userID)
        {
            userWordValueCounter = 0;
            usertweethistory = db.GetUserTweetByUserID(userID);

            TestDictionary = new WordClassification();
            /*Tests*/
            int index = 0;
            /*Tests*/
            foreach (Tweet tweet in usertweethistory)
            {
                index++;
                tweetWithoutPunctuation = GetStringWithoutPunctuation(tweet.Text);
                tweetWithoutPunctuation = tweet.Text.Replace("http://", "").Replace("https://", "").Replace("RT", "").Replace("@", "");
                CompareSentenceEmoticonsArray(tweetWithoutPunctuation);

                // breack the sentence to combinations of maxWordInSubSentence and compare to db dictionary
                senctence = Global.SplitSentenceToSubSentences(tweetWithoutPunctuation, maxWordInSubSentence);
                CompareSentenceToGenderDictionary(senctence);
                /*Tests*/
                Console.WriteLine("Tweet number {0} from {1}", index, usertweethistory.Count);
                /*Tests*/
                if (index > 200)
                    break;
            }
            /*Tests*/
            Console.WriteLine("Counter =" + userWordValueCounter);
            TestDictionary.print();
            /*Tests*/

            SaveUserGenderScoreToDB(userID);
        }

        private void SaveUserGenderScoreToDB(long userID)
        {

            if (userWordValueCounter > 0)
            {
                db.UpdateUserPropertiesByUserID("GenderID", (int)Common.Gender.FEMALE, userID);
                Console.WriteLine("User ID : {0} is Female", userID);
                Console.WriteLine("Counter =" + userWordValueCounter);
            }     
            else if (userWordValueCounter < 0)
            {
                db.UpdateUserPropertiesByUserID("GenderID", (int)Common.Gender.MALE, userID);
                Console.WriteLine("User ID : {0} is Male", userID);
                Console.WriteLine("Counter =" + userWordValueCounter);
            }

            else if (userWordValueCounter == 0)
            {
                db.UpdateUserPropertiesByUserID("GenderID", (int)Common.Gender.Unknown, userID);
                return;
            }

        }

        /// <summary>
        /// This function compare each word or words combination from
        /// the original sentence to dictionary Age Value
        /// </summary>
        /// <param name="splitSentence"></param>
        private void CompareSentenceToGenderDictionary(List<string> splitSentences)
        {
            int index = 0;
            foreach (string word in splitSentences)
            {
                index++;
                double returnValue = 0;
                try
                {
                    //Console.WriteLine("Word is {0}", word);
                    returnValue = db.GetGenderValueByWord(word.Replace("'", "''"));
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
                if (returnValue != 0) //need to check empty  
                {
                    TestDictionary.IncreaseWord(word, returnValue , 1);
                    userWordValueCounter += returnValue;
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

            foreach (WordGender Emoticon in emoticonsArrayValue)
            {
                if (Sentence.Contains(Emoticon.Word))
                {
                    userWordValueCounter += Emoticon.WordRate;
                    TestDictionary.IncreaseWord(Emoticon.Word, Emoticon.WordRate, 1);
                }
            }//foreach
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
            for (int i = 0; i < oldString.Length; i++)
            {
                char c = oldString[i];
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
                    || (i != 0 && i != oldString.Length - 1 && char.IsLetter(oldString[i - 1]) && char.IsLetter(oldString[i + 1]) && c.In('-', '\'')))
                    newString.Append(c);
            }
            return newString.ToString();
        }
    }
}
