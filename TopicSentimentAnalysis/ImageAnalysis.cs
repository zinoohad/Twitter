using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;
using System.Net;
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

        public FaceDetectObject GetFaceDetectAndImageAnalysis(string imageURL)
        {
            string json = PostIBM(imageURL);
            FaceDetectObject f = JsonConvert.DeserializeObject<FaceDetectObject>(json);
            return f;
        }

        public string PostIBM(string imageUrl)
        {
            // Create a request using a URL that can receive a post.   
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://visual-recognition-demo.mybluemix.net/api/classify");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
          
            string postData = string.Format("classifier_id=&use--example-images=someImage&url={0}&image_data=", imageUrl);
            byte[] byteArray = Encoding.Default.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.Headers.Add("authorization", "Basic YWNjXzJkYzdkNzNjMmYwODliMToxYzQ3Yzg2ZDg0YjdmYjdjYjZjNzQ1NTQ1MmYwNTgzMQ==");
            request.Referer = "https://visual-recognition-demo.mybluemix.net/";
            // Set the ContentLength property of the WebRequest.  

            request.ContentLength = byteArray.Length;

            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("_csrf", "DxU24Nqe4lK-aax5kPymRx0n") { Domain = "visual-recognition-demo.mybluemix.net" }); // { Domain = "imagga.com" }
            request.CookieContainer.Add(new Cookie("TLTSID", "IsqGOq58Fzww2P1lwvDUzs8bCiR1o3oa") { Domain = "visual-recognition-demo.mybluemix.net" });
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  

            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
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

        public FImaggaObject GetImageAnalysisFreeImagga(string imageUrl)
        {
            string result = GetImaggaFreeRequest(imageUrl);
            FImaggaObject image = JsonConvert.DeserializeObject<FImaggaObject>(result);
            return image;
        }

        private string GetImaggaFreeRequest(string imageUrl)
        {
            var cookieContainer = new CookieContainer();
            string token = GetToken(imageUrl, ref cookieContainer);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://imagga.com/auto-tagging-demo/query");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.              
            //string postData = string.Format("url={0}&_token=DK0TlwfmeUsLfQErROMo0vJJLayPmkfdSr24WQIz&color_results=0&language=en", imageUrl);
            string postData = string.Format("url={0}&_token={1}&color_results=0&language=en", imageUrl, token);
            byte[] byteArray = Encoding.Default.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.Headers.Add("authorization", "Basic YWNjXzJkYzdkNzNjMmYwODliMToxYzQ3Yzg2ZDg0YjdmYjdjYjZjNzQ1NTQ1MmYwNTgzMQ==");
            request.Referer = string.Format("https://imagga.com/auto-tagging-demo?url={0}",imageUrl);
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            
            // Set cookie
            request.CookieContainer = cookieContainer;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        private string GetToken(string imageUrl, ref CookieContainer cookie)
        {
            //string responseFromServer = new WebClient().DownloadString(string.Format("https://imagga.com/auto-tagging-demo?url={0}", imageUrl));

            // Try to get cookie
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://imagga.com/auto-tagging-demo?url={0}", imageUrl));
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  

            string responseFromServer = reader.ReadToEnd();

            // Get Token
            string token = ReturnSubStringFromTo(responseFromServer, "<input type=\"hidden\" name=\"_token\" value=\"", "\">");

            // Get Cookie Set from request header
            string cookieSet = response.Headers.Get("Set-Cookie");

            string XSRFTOKEN = ReturnSubStringFromTo(cookieSet, "XSRF-TOKEN=", "; ");
            string immagaSession = ReturnSubStringFromTo(cookieSet, "imagga_session=", "; ");
            string httpOnly = ReturnSubStringFromTo(cookieSet, "; httponly,", "=");

            // Create a cookies
            cookie.Add(new Cookie("_ga", "GA1.2.672366569.1482924020") { Domain = "imagga.com" });
            cookie.Add(new Cookie("_hjIncludedInSample", "1") { Domain = "imagga.com" });
            cookie.Add(new Cookie("_hjMinimizedPolls", "\"16756,26447\"") { Domain = "imagga.com" });
            cookie.Add(new Cookie(httpOnly, ReturnSubStringFromTo(cookieSet,httpOnly + "=","; ")) { Domain = "imagga.com" });
            cookie.Add(new Cookie("imagga_session", immagaSession) { Domain = "imagga.com", Path = "/" });
            cookie.Add(new Cookie("intercom-id-mqpky8d2", "0c11285d-2988-4009-81a5-7f17eac90b42") { Domain = ".imagga.com" });
            cookie.Add(new Cookie("XSRF-TOKEN", XSRFTOKEN) { Domain = "imagga.com", Path = "/" });

            return token;
        }

        private string ReturnSubStringFromTo(string input, string From, string To)
        {
            int FromInt = input.LastIndexOf(From) + From.Length;
            int ToInt = input.IndexOf(To, FromInt);
            string token = input.Substring(FromInt, ToInt - FromInt);
            return token;
        }

        #endregion

    }
}
