using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TopicSentimentAnalysis.Classes;

namespace TopicSentimentAnalysis
{
    public class ImageAnalysis
    {
        #region IBM
        private string Host = "https://watson-api-explorer.mybluemix.net/";
        private string APIKey = "cebb51c40045337979791dff98e0d24545b3e37a";
        private string GetIBMRequest(string URL)
        {     
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL);
            //request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        public ImageAnalysisObject GetImageAnalysisIBM(string picURL)
        {
            string URL = Host + string.Format("visual-recognition/api/v3/classify?api_key={0}&url={1}&owners=IBM%2Cme&classifier_ids=default&version=2016-05-20", APIKey, picURL);
            string result = GetIBMRequest(URL);
            ImageAnalysisObject image = JsonConvert.DeserializeObject<ImageAnalysisObject>(result);
            return image;
        }
        public FaceDetectObject DetectFacesIBM(string imageURL)
        {
            string URL = Host + string.Format("visual-recognition/api/v3/detect_faces?api_key={0}&url={1}&version=2016-05-20", APIKey, imageURL);
            string result = GetIBMRequest(URL);
            FaceDetectObject image = JsonConvert.DeserializeObject<FaceDetectObject>(result);
            return image;
        }
        #endregion
        #region Imagga        
        private string ImaggaApiKey = "acc_6d0e280e5388072";
        private string ImaggaApiSecret = "baeba2b0dfde16222a44b3bcb0f58c07";
        private string GetImaggaRequest(string imageUrl)
        {
            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", ImaggaApiKey, ImaggaApiSecret)));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.imagga.com/v1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", basicAuthValue));

                HttpResponseMessage response = client.GetAsync(String.Format("tagging?url={0}", imageUrl)).Result;

                HttpContent content = response.Content;
                string result = content.ReadAsStringAsync().Result;
                return result;
            }
        }
        public ImaggaObject GetImageAnalysisImagga(string imageUrl)
        {
            string result = GetImaggaRequest(imageUrl);
            ImaggaObject image = JsonConvert.DeserializeObject<ImaggaObject>(result);
            return image;

        }
        #endregion
    }
}
