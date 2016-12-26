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
using TopicSentimentAnalysis;
using TopicSentimentAnalysis.Classes;
using System.Threading;
using Twitter.Controllers;
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
            var twitter = new TwitterAPI();
            User u = twitter.GetUserProfile("", 185260477);
            var a = twitter.GetUserFavoritesTweets("Barrefaeli");

        }
      
    }
}
