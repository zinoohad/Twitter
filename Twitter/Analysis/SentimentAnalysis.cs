
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace Twitter.Analysis
{
    public class SentimentAnalysis
    {
        #region Params
        string AuthenticationKey = "ade760cc94704e9a224e9323d63467b1"; // Authentication 
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        string Model = "general_en";
        private Encoding requestEncoding = Encoding.GetEncoding(1255);  //Hebrew
        private const string SentimentAnalysisAddress = "http://api.meaningcloud.com/sentiment-2.1";//SentimentAnalysis API Address
        #endregion


        /// <summary>
        /// replay sentence value by sending http request to api.meaningcloud.com
        /// </summary>
        /// <param name="Text"> refer to what sentence to value </param>
        public string GetAnalysis(string Text)
        {
            /*--- Send Http request with text---*/
            string content = "key=" + AuthenticationKey + "&of=json&txt=" + Text + ".&model=general&lang=en", jsonStr;
            Sentence_object SentenceResponse = new Analysis.Sentence_object();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, SentimentAnalysisAddress);
            request.Content = new StringContent(content, requestEncoding, "application/x-www-form-urlencoded");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            /*--- Read respone to json---*/
            jsonStr = response.Content.ReadAsStringAsync().Result;

            /*--- Convert json to SentenceResponse calss ---*/
            try
            {
                SentenceResponse = serializer.Deserialize<Sentence_object>(jsonStr);
            }
            catch (InvalidExpressionException e)
            {
                // Print e?<<-----------------------------------------------------------------------------------------------------------------------------------------------------
            }
            /*--- Check if http request Succeeded ---*/
            if (SentenceResponse.status.code=="0")
            {
                string sentenceValue = SentenceResponse.score_tag;
                switch (sentenceValue)
                {
                    case "P +":
                        return "strong positive";
                    case "P":
                        return "positive";
                    case "NEU":
                        return "neutral";
                    case "N":
                        return "negative";
                    case "N +":
                        return "strong negative";
                    default:
                        return "neutral";
                } //switch                  
            }//if
            /*--- http request not Succeeded ---*/
            return "error code:" + SentenceResponse.status.code;
        }
    }

}
