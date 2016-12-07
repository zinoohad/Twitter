using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using Twitter.Analysis;

namespace Twitter.Analysis
{
    class TopicAnalysis
    {
        #region Params
        string AuthenticationKey = "ade760cc94704e9a224e9323d63467b1";
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        string Model = "general_en";
        private Encoding requestEncoding = Encoding.GetEncoding(1255);  //Hebrew
        private const string SentimentAnalysisAddress = "http://api.meaningcloud.com/sentiment-2.1"; //SentimentAnalysis API Address
        #endregion


        /// <summary>
        /// replay topic anaylsis by returen 2 lists ( concept, Entity) by sending http request to api.meaningcloud.com
        /// </summary>
        /// <param name="Text"> refer to what topic to extract </param>
        public string GetAnalysis(string Text)
        {
            /*--- Send Http request with text---*/
            string content = "key=" + AuthenticationKey + "&of=json&lang=en&ilang=en&txt=" + Text + ".&tt=a&uw=y", jsonStr;
            Topicobject TopicResponse = new Topicobject();
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
                TopicResponse = serializer.Deserialize<Topicobject>(jsonStr);
            }
            catch (InvalidExpressionException e)
            {
                // Print e?<<-----------------------------------------------------------------------------------------------------------------------------------------------------
            }
            /*--- Check if http request Succeeded ---*/
            if (TopicResponse.status.code == "0")
            {  
                string Topics = "";
                for(int i=0;i< TopicResponse.sentimented_concept_list.Count;i++)
                    Topics= Topics + "Concept " + i+": "+ TopicResponse.sentimented_concept_list[i].form+" | ";
                for (int i = 0; i < TopicResponse.sentimented_entity_list.Count; i++)
                    Topics = Topics + "Entity " + i + ": " + TopicResponse.sentimented_entity_list[i].form + " |  ";
                return Topics;
            }//if
            /*--- http request not Succeeded ---*/
            return "error code:" + TopicResponse.status.code;
        }
    }
}
