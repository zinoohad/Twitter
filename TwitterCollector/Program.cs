﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using DataBaseConnections;
using System.Windows.Forms;
using TwitterCollector.Forms;
using TwitterCollector.Controllers;
using Receptiviti.Client;
using TopicSentimentAnalysis;
using TwitterCollector.Threading;
using System.IO;

namespace TwitterCollector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ImageAnalysis i = new ImageAnalysis();
            var result = i.GetImageAnalysisFreeImagga("http://www.slate.com/content/dam/slate/blogs/xx_factor/2014/susan.jpg.CROP.promo-mediumlarge.jpg");
            //i.GetImageAnalysisIBM("https://pbs.twimg.com/profile_background_images/695841058/a00189f47992007bfa0cc8a13fba107e.jpeg");
            //i.DetectFacesIBM("https://pbs.twimg.com/profile_background_images/695841058/a00189f47992007bfa0cc8a13fba107e.jpeg");

            //(new SentimentAnalysis()).Start();

            //(new Supervisor()).Start();

            List<string> sentence = Global.SplitSentenceToSubSentences("the punishment assigned to a defendant found guilty by a court", 30);
            var s = WordSentimentAnalysis.CheckWordAge(sentence.ToArray());

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //new CMain();
            //Application.Run();

            DBHandler dbh = Global.DB;
            dbh.UpdateDictionaryAge();
            //List<string> data = WebHandler.ReadWebPage("http://www.urbandictionary.com/popular.php?character=A");
            //List<string> data = WebHandler.ReadWebPage("http://time.com/4373616/text-abbreviations-acronyms/");



            //CMain controller = new CMain();

        }
    }
}
