using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows.Forms;
using TweetSharp;
using Twitter.Classes;
using Twitter.Classes.Navigators;
using Twitter.Common;
using Twitter.Forms;

namespace Twitter
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TwitterResultDisplay());
            //test();
        }    
        public static void test()
        {
            var twitter = new Twitter
            {
                OAuthConsumerKey = ConfigurationSettings.AppSettings["OAuthConsumerKey"],
                OAuthConsumerSecret = ConfigurationSettings.AppSettings["OAuthConsumerSecret"]
            };

            //List<long> ids = twitter.GetFriendsIDs("BarRefaeli").Result;
            int count = 0;
            var SERVICE = new TwitterService(ConfigurationSettings.AppSettings["OAuthConsumerKey"],
                ConfigurationSettings.AppSettings["OAuthConsumerSecret"]);
            SERVICE.AuthenticateWith(ConfigurationSettings.AppSettings["AccessKey"], ConfigurationSettings.AppSettings["AccessSecret"]);
            var otweet = new ListFollowersOptions();
            otweet.ScreenName = "@BarRefaeli";
            var res = SERVICE.ListFollowers(otweet);
            List<Tweets> twitts = twitter.SearchTweets("אש");
            //List<Tweets> stwitter = twitter.GetTweets("@netanyahu").Result;
            //List<Tweets> s1twitter = twitter.GetTweets("@BarRefaeli").Result;
            //FriendsNavigator ff = twitter.GetFriendsIDs("@BarRefaeli").Result;
           // List<Users> u = twitter.GetFriends("@haifacity",false,200).Result;
            //twitts = twitter.GetTwitts(26793734, 20).Result;
            //foreach (var t in twitts)
            //{
            //    count++;
            //    Console.WriteLine(t + "\n");
            //}
        }
    }
}
