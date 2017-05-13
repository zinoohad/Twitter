using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using TopicSentimentAnalysis.Classes;
using Newtonsoft.Json;

namespace TopicSentimentAnalysis
{
    public static class WordSentimentAnalysis
    {
        public static List<WordAge> CheckWordAge(params string[] words)
        {
            words = words.Distinct().ToArray();
            string json = PostRequest(words);
            AgeResultNavigator arn = JsonConvert.DeserializeObject<AgeResultNavigator>(json);
            List<WordAge> result = new List<WordAge>();
            if (arn == null || arn.WordsSet.Count == 0) return result;
            for (int i = 0; i < arn.WordsSet.Count; i++)
                result.Add(new WordAge(arn.WordsSet[i], arn.AgeResultList[i]));
            return result;
        }

        private static string PostRequest(string[] words)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("http://wwbp.org/getWordData.php");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            string Cookie = "XS4ubaN05BoP9yA";
            string postData =
                "------WebKitFormBoundary" + Cookie + Environment.NewLine +
                "Content-Disposition: form-data; name=\"searchField\"" + Environment.NewLine + Environment.NewLine + 

                string.Join(", ",words) + Environment.NewLine +
                //"wonderful, I am thankful, :), excited" + Environment.NewLine +
                "------WebKitFormBoundary" + Cookie + Environment.NewLine +
                "Content-Disposition: form-data; name=\"robustnessIterations\"" + Environment.NewLine + Environment.NewLine +

                "1" + Environment.NewLine +
                "------WebKitFormBoundary" + Cookie + Environment.NewLine +
                "Content-Disposition: form-data; name=\"bandwidth\"" + Environment.NewLine + Environment.NewLine +

                ".6" + Environment.NewLine +
                "------WebKitFormBoundary"  + Cookie + "--";

            byte[] byteArray = Encoding.Default.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundary" + Cookie;
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
