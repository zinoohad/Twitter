using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace TwitterCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            var twitter = new Twitter
            {
                OAuthConsumerKey = "EHWLWeAGxRmudqjie56c4TFzP",
                OAuthConsumerSecret = "1C4szWqlYexK1LuQuq0AA27ckfIrd0jXwfvDGZm40ERz6aHrmQ"
            };
            int count = 0;
            //IEnumerable<string> twitts = twitter.GetTwitts("@BarRefaeli", 300).Result;
            var twitts = twitter.GetFriends("@BarRefaeli").Result;
            foreach (var t in twitts)
            {
                count++;
                Console.WriteLine(t + "\n");
            }

            //using (WebClient webClient = new WebClient())
            //{
            //    webClient.DownloadFile("http://twlets.com/user-tweets?screen_name=BarRefaeli&maxtweet=3200", @"C:\Users\zinoo\Desktop\Testing\test.csv");
            //}

            //getTweet();
            
        }
        private static void getTweet()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://twlets.com/user-tweets?screen_name=BarRefaeli&maxtweet=3200");
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            //Stream responseStream = webResponse.GetResponseStream();
            //StreamReader streamReader = new StreamReader(responseStream);
            //string s = streamReader.ReadToEnd();

            using (Stream output = File.OpenWrite(@"C:\Users\zinoo\Desktop\Testing\test.xlsx"))
            using (Stream input = webResponse.GetResponseStream())
            {
                input.CopyTo(output);
            }
        }
    }
}
