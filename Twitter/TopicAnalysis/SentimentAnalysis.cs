using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Twitter.Classes;
using Twitter.Classes.Navigators;
using Twitter.Interface;

namespace Twitter
{
    public class SentimentAnalysis
    {
        #region Params
        string AuthenticationKey = "ade760cc94704e9a224e9323d63467b1";
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        string Model = "general_en";
        private Encoding requestEncoding = Encoding.GetEncoding(1255);  //Hebrew
        private const string SentimentAnalysisAddress = "http://api.meaningcloud.com/sentiment-2.1";//SentimentAnalysis API Address
        #endregion



        public string GetAnalysis(string Text)
        {
            /*--- Test 1*/
            string content = "key=" + AuthenticationKey + "&of=json&txt=" + Text + ".&model=general&lang=en", jsonStr;
            TopicAnalysis.Sentence_object jsonNot = new TopicAnalysis.Sentence_object();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, SentimentAnalysisAddress);
            request.Content = new StringContent(content, requestEncoding, "application/x-www-form-urlencoded");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            jsonStr = response.Content.ReadAsStringAsync().Result;


            try
            {
                jsonNot = serializer.Deserialize<TopicAnalysis.Sentence_object>(jsonStr);
            }
            catch (InvalidExpressionException e)
            {
                // Print e?
            }
            if(jsonNot.status.code=="0")
            {
                string sentenceValue = jsonNot.score_tag;
                /*
              P +: strong positive
              P:positive
              NEU: neutral
              N: negative
              N +: strong negative
              NONE: without sentiment
                */
                switch (sentenceValue)
                {
                    case "P +":
                        return "strong positive";
                        break;
                    case "P":
                        return "positive";
                        break;
                    case "NEU":
                        return "neutral";
                        break;
                    case "N":
                        return "negative";
                    case "N +":
                        return "strong negative";
                        break;
                    default:
                        return "neutral";
                        break;
                }
                    

            }
               
            return "error code:"+jsonNot.status.code;

        }
    }

}
