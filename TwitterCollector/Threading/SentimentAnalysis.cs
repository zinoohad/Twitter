using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicSentimentAnalysis;
using TopicSentimentAnalysis.Classes;
using Twitter.Classes;
using TwitterCollector.Common;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    public class SentimentAnalysis : BaseThread
    {

        #region Params

        private ApiKeys apiKey;

        private APIConnection sentementAnalysis;

        #endregion

        public override void RunThread()
        {
            List<Tweet> tweets;
            while (ThreadOn)
            {
                try
                {
                    tweets = db.GetTweetsToCheckSentementAnalysis(ThreadType.SENTIMENT_ANALYSIS);
                    foreach(Tweet t in tweets)
                        UseSentimentAnalysisAPI(t);

                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }   
        }

        private void UseSentimentAnalysisAPI(Tweet tweet)
        {
            Analysis a;

            if (!SetApiConnection()) 
                return;            

            try
            {
                a = sentementAnalysis.GetSentimentAnalysis(tweet.Text);

                // Learn new words using Age API
                Global.AddSentenceToBufferForChecking(tweet.Text);
            }
            catch (InvalidExpressionException e)
            {
                new TwitterException(string.Format("Exception was thrown while try to send sentement analysis request: '{0}'.", e.Message));
                return;
            }

            apiKey.RemainingCredits = a.status.remaining_credits;
            if (!a.status.msg.Equals("OK")) //Problem with the request
                return;

            db.SaveTweetScore(tweet, a.score_tag, a.confidence);
            //if (a.sentimented_concept_list != null && a.sentimented_concept_list.Count > 0)
            //    LearnNewWords(a.sentimented_concept_list);
            //else
            LearnNewWords(a.sentence_list);
        }

        public void LearnNewWords(IList<Concept> concept)
        {
            if (concept == null || concept.Count == 0)
                return;

            foreach (Concept c in concept)
            {
                if (c.NotIn("NEU", "NONE"))
                    db.LearnNewPosNegWord(c);
            }
        }

        public void LearnNewWords(IList<Sentence> sentence)
        {
            if (sentence == null || sentence.Count == 0)
                return;

            foreach (Sentence s in sentence)
            {
                foreach (Segment sg in s.segment_list)
                {
                    foreach (PolarityTerm p in sg.polarity_term_list)
                    {
                        if (p.NotIn("NEU", "NONE"))
                            db.LearnNewPosNegWord(p);
                    }
                }
            }
        }

        private bool SetApiConnection()
        {
            if (apiKey != null)
                db.UpdateRemainingCredits(ref apiKey);

            if (apiKey == null || apiKey.RemainingCredits == 0)
            {
                if (apiKey != null)
                    db.UpdateRemainingCredits(ref apiKey);
                apiKey = db.GetApiKey(ExternalAPI.MeaningCloud);
                if (apiKey != null)
                    sentementAnalysis = new APIConnection(apiKey.Key1);
                else
                {
                    new TwitterException("The sentement analysis keys bucket is empty.");
                    return false;
                }
            }
            return true;
        }
    }
}
