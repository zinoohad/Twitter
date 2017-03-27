using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using TopicSentimentAnalysis.Classes;

namespace TopicSentimentAnalysis
{
    public class APIConnection
    {
        #region Params

        private string _AuthenticationKey { get; set; } // Authentication 

        //private string AuthenticationKey = "ade760cc94704e9a224e9323d63467b1"; // Authentication 

        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private Encoding requestEncoding = Encoding.GetEncoding(1255);  //Hebrew

        private const string SentimentAnalysisAddress = "http://api.meaningcloud.com/sentiment-2.1";    //SentimentAnalysis API Address

        #endregion

        public APIConnection(string AuthenticationKey = null)
        {
            if (AuthenticationKey == null)
                _AuthenticationKey = "ade760cc94704e9a224e9323d63467b1";
            else
                _AuthenticationKey = AuthenticationKey;
        }

        /// <summary>
        /// Get request by sending a text string and return in json form.
        /// </summary>
        /// <param name="text">String send to the API</param>
        /// <returns>Return json</returns>
        private string GetRequest(string Text, RequestType rt)
        {
            string content;
            if (rt == RequestType.SentimentAnalysis)
                content = "key=" + _AuthenticationKey + "&of=json&txt=" + Text + ".&model=general&lang=en";
            else
                content = "key=" + _AuthenticationKey + "&of=json&lang=en&txt=" + Text + ".&tt=a&uw=y";
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, SentimentAnalysisAddress);
            request.Content = new StringContent(content, requestEncoding, "application/x-www-form-urlencoded");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// The text provided is analyzed to determine if it expresses a positive/negative/neutral sentiment; 
        /// to do this, the local polarity of the different sentences in the text is identified and the relationship between them evaluated, 
        /// resulting in a global polarity value for the whole text.
        /// </summary>
        /// <param name="Text">String that contains the text to analysis.</param>
        /// <returns>Analysis object contains all the parameters that was analysis on this subject.</returns>
        /// <exception cref="InvalidExpressionException">If the text wasn't in the right format or thier is problem in the request syntax.</exception>
        public Analysis GetSentimentAnalysis(string Text)
        {
            Analysis a = new Analysis();
            string jsonStr = GetRequest(Text, RequestType.SentimentAnalysis);
            if(string.IsNullOrEmpty(jsonStr)) return null;
            a = serializer.Deserialize<Analysis>(jsonStr);
            return a;

            //if (SentenceResponse.status.code == "0")
            //{
            //    string sentenceValue = SentenceResponse.score_tag;
            //    switch (sentenceValue)
            //    {
            //        case "P +":
            //            return "strong positive";
            //        case "P":
            //            return "positive";
            //        case "NEU":
            //            return "neutral";
            //        case "N":
            //            return "negative";
            //        case "N +":
            //            return "strong negative";
            //        default:
            //            return "neutral";
            //    } //switch                  
            //}//if
            ///*--- http request not Succeeded ---*/
            //return "error code:" + SentenceResponse.status.code;
        }

        public Topic GetTopicAnalysis(string Text)
        {
            Topic t = new Topic();
            string jsonStr = GetRequest(Text, RequestType.TopicAnalysis);
            if (string.IsNullOrEmpty(jsonStr)) return null;
            t = serializer.Deserialize<Topic>(jsonStr);
            return t;

            //if (TopicResponse.status.code == "0")
            //{
            //    string Topics = "";
            //    for (int i = 0; i < TopicResponse.sentimented_concept_list.Count; i++)
            //        Topics = Topics + "Concept " + i + ": " + TopicResponse.sentimented_concept_list[i].form + " | ";
            //    for (int i = 0; i < TopicResponse.sentimented_entity_list.Count; i++)
            //        Topics = Topics + "Entity " + i + ": " + TopicResponse.sentimented_entity_list[i].form + " |  ";
            //    return Topics;
            //}//if
            ///*--- http request not Succeeded ---*/
            //return "error code:" + TopicResponse.status.code;
        }

    }
    public enum RequestType
    {
        TopicAnalysis,
        SentimentAnalysis
    }
}
