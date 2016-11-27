using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Twitter.Classes;
using Twitter.Classes.Navigators;
using Twitter.Common;

namespace Twitter
{
    class Program
    {
        static void Main(string[] args)
        {

            var twitter = new Twitter
            {
                OAuthConsumerKey = ConfigurationSettings.AppSettings["OAuthConsumerKey"],
                OAuthConsumerSecret = ConfigurationSettings.AppSettings["OAuthConsumerSecret"]
                //OAuthConsumerKey = "EHWLWeAGxRmudqjie56c4TFzP",
                //OAuthConsumerSecret = "1C4szWqlYexK1LuQuq0AA27ckfIrd0jXwfvDGZm40ERz6aHrmQ"
            };
            int count = 0;
            //List<Tweets> twitts = twitter.GetTwitts("@BarRefaeli", 2500).Result;
            //FriendsNavigator ff = twitter.GetFriendsIDs("@BarRefaeli").Result;
            List<Users> u = twitter.GetFriends("@haifacity",false,200).Result;
            //twitts = twitter.GetTwitts(26793734, 20).Result;
            //foreach (var t in twitts)
            //{
            //    count++;
            //    Console.WriteLine(t + "\n");
            //}
            
        }      
    }
}
