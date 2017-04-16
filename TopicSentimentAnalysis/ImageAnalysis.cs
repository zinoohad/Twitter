using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://imagga.com/auto-tagging-demo/query");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            
            // Create POST data and convert it to a byte array.  
            //string postData = "url=http%3A%2F%2Fwww.slate.com%2Fcontent%2Fdam%2Fslate%2Fblogs%2Fxx_factor%2F2014%2Fsusan.jpg.CROP.promo-mediumlarge.jpg&_token=JWJnROUR4KzZmAxRBniPXJVcXpApby60Nt51sg6k&color_results=0&language=en";
            string postData = string.Format("url={0}&_token=DK0TlwfmeUsLfQErROMo0vJJLayPmkfdSr24WQIz&color_results=0&language=en", imageUrl);
            byte[] byteArray = Encoding.Default.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.Headers.Add("authorization", "Basic YWNjXzJkYzdkNzNjMmYwODliMToxYzQ3Yzg2ZDg0YjdmYjdjYjZjNzQ1NTQ1MmYwNTgzMQ==");
            request.Referer = string.Format("https://imagga.com/auto-tagging-demo?url={0}",imageUrl);
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            
            // Create a cookie
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("_ga", "GA1.2.672366569.1482924020") { Domain = "imagga.com" });
            request.CookieContainer.Add(new Cookie("_hjIncludedInSample", "1") { Domain = "imagga.com" });          
            request.CookieContainer.Add(new Cookie("_hjMinimizedPolls", "\"16756,26447\"") { Domain = "imagga.com" });
            request.CookieContainer.Add(new Cookie("0bed65303b7e41d6dd5655b89607800c697fa36b","eyJpdiI6InJmUUs1STkwUFJEUWFoNDNwNEd0c2c9PSIsInZhbHVlIjoiUk1nUHpqR1RhZFJwVCs1SjFtR01qVVU3TGdrd2JBdlY0KzJpTlVVMGlZeVBobWdsM1ZWS3lKUzNabm16d1NKKzFVWXQ2aVVvV25MVlZcL0pYSW5qWkNMd1Z3cktvb2wyTE9LaTR3MktOVVJcL1R0VWZTekpFc0tRRTB0XC9USkpWWkFVODRaSTRka0tPWUxxTkNzcTk2YzYxczlrV1FhbEptbTloNmJqQlBPMHQ2YUhVT0YxZ1BlbDZFODBkZktiY0ZrYmEwQW96WE9YbjdrXC9oR1FDYjRCXC9SRVZDczVyam94RWRKckU5OXRKWFdlbTFkWmlsYlRcLzRoaENLeUFWV09lS2ttUE02WFRHQlJMdk01aGZPUFUwcHBOWElocFpwVjZMM2tWemNqTlhGczZRQThMZ0ZhVnJxUDNjckJVZm5ZbjJlK2VtOVpHN2dFNXhyNXNmR0FzM1RoTjk1cFhxU0ZVOGkwNGJwYWRlT2QwVWZTeFpld2Z6QUxtMVQ3R1pZejBqRndDT0ZROHRtUjB3c29UM2dVUzV3NU9mSFwvOWU4NVVnNGQza3JmOTFnZVY5WXpZMEpVZzlcL0dRbjE0YWIxY3lrbkxDTmlsNzlwWFd6dkZKTTBqTE5rYVJZSjFuV1lwckZOdWJEeDdybU1OVmdoU0RVSEtKNlwvWnRaQk44eUJJaGZiSzZcL1JpUHJEOXVQQVNhak5nb0t1aXhPU3h4dGlQTVV0SEtOZlEyb1A1QTR6b0Q3RWJTMEJObEVFTkZ3aVNvOUxXV2RUVmlUYlBSVXJmQVVXQWdSUGxmdFFoWXJjUlc0OU94NWJPQVRcL3A4VmYwejYyYzRcLzNUcFwvUndhZjUxTXUiLCJtYWMiOiJiZTAyYmRhNzU5N2IwMWJlYzk1NmQxMWRhNmMxZmRkZjY2NTQyOTBjM2Y5N2UyYTlmNGI4OWU4OWJlNDQ2NmRhIn0%3D") { Domain = "imagga.com" });
            request.CookieContainer.Add(new Cookie("imagga_session","eyJpdiI6Im43aXRyWGZCOHBxTzdWQnZDQTVBaHc9PSIsInZhbHVlIjoiSDlFNkNhZ1hcL1dRV29tZUNzN1oxTlBvK1lDNmtEYXRMQmZpVjRVZ1o2Y1czSnoxdkxwMEtCRGkzdjEwd3ptaUFndWVyNXVPVWRxK1wvSmVmUFpieXUrQT09IiwibWFjIjoiY2ZhMGVkMTgxMzU2MTdjZWM5MGNmZDc4YTk2MzM5Njk2OWJkZmUwMmYyYWU3NzMyZThkNjk0MWJlNWM2YmQwYSJ9") { Domain = "imagga.com", Path = "/" });
            request.CookieContainer.Add(new Cookie("intercom-id-mqpky8d2", "0c11285d-2988-4009-81a5-7f17eac90b42") { Domain = ".imagga.com" });
            request.CookieContainer.Add(new Cookie("XSRF-TOKEN", "eyJpdiI6InZ6akhENVpGUDQ5NlJaXC9WWlhMQlJBPT0iLCJ2YWx1ZSI6ImVLQ085RnpGS2VmWlFBNWQ4NURhMmtCNkxrTzdsUlJOZjI2NFRjVHhLbVBqWjVwejNOK2t2Z0U1RlwvQTJpXC9LeXlnQ2k2VVcwWXdXM0tNMXRMTGhtSHc9PSIsIm1hYyI6Ijg1M2RmMGVmYjNlMzk3Yzg4YjFjYWRmZjg2Y2Y2YWYzNzg0YzkyMjUwNjk5NTc4YzgyZDQ0ZmY0ODVhYzdmZDIifQ%3D%3D") { Domain = "imagga.com", Path = "/" });
            //request.CookieContainer.Add(new Cookie("_gat", "1") { Domain = "imagga.com" });
            

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
        #endregion
    }
}
