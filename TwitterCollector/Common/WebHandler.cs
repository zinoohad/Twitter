using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TwitterCollector.Common
{
    public static class WebHandler
    {
        public static List<string> ReadWebPage(string urlAddress)
        {
            //string data = "";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    Stream receiveStream = response.GetResponseStream();
            //    StreamReader readStream = null;

            //    if (response.CharacterSet == null)
            //        readStream = new StreamReader(receiveStream);                
            //    else
            //        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            //    data = readStream.ReadToEnd();

            //    response.Close();
            //    readStream.Close();
            //}
            //return data;
            List<string> words = new List<string>();
            var client = new WebClient();
            using (var stream = client.OpenRead(urlAddress))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("<span class=\"s2\"><b>"))
                    {
                        //string[] splitVal = { ">", "</a>" };
                        string[] splitVal = { "<span class=\"s2\"><b>", ":" };
                        string[] split = line.Split(splitVal, StringSplitOptions.RemoveEmptyEntries);
                        string word = split[1].Replace("&#8217;", "'");
                        if (word.Contains('/'))
                        {
                            words.AddRange(word.Split('/'));
                        }
                        else words.Add(word);
                    }
                    else if (line.Contains("<p><strong>") && line.Contains(":</strong>"))
                    {
                        string[] splitVal = { "<p><strong>", ":</strong>" };
                        string[] split = line.Split(splitVal, StringSplitOptions.RemoveEmptyEntries);
                        string word = split[0].Replace("&#8217;", "'");
                        if (word.Contains('/'))
                        {
                            words.AddRange(word.Split('/'));
                        }
                        else if (word.Contains(" or "))
                        {
                            string[] splitVall = { " or " };
                            words.AddRange(word.Split(splitVall, StringSplitOptions.RemoveEmptyEntries));
                        }
                        else words.Add(word);
                    }
                }
            }
            return words;
        }
    }
}
