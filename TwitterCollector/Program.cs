using System;
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
using System.IO;

namespace TwitterCollector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {



            TwitterCollector.Threading.ImageAnalysis i = new TwitterCollector.Threading.ImageAnalysis();
            i.Start();
            //var r = i.GetFaceDetectAndImageAnalysis("https://pbs.twimg.com/profile_images/845225405711876096/1KHWivUZ.jpg");
            //var result = i.GetImageAnalysisFreeImagga("https://s-media-cache-ak0.pinimg.com/736x/61/56/13/615613ca825bd973f977b60865a22e2c.jpg");
            //i.GetImageAnalysisIBM("https://pbs.twimg.com/profile_background_images/695841058/a00189f47992007bfa0cc8a13fba107e.jpeg");
            //i.DetectFacesIBM("https://pbs.twimg.com/profile_background_images/695841058/a00189f47992007bfa0cc8a13fba107e.jpeg");

            //(new SentimentAnalysis()).Start();

            //(new Supervisor()).Start();

            //List<string> sentence = Global.SplitSentenceToSubSentences("the punishment assigned to a defendant found guilty by a court", 30);
            //var s = WordSentimentAnalysis.CheckWordAge(sentence.ToArray());

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //new CMain();
            //Application.Run();

            //DBHandler dbh = Global.DB;
            //dbh.UpdateDictionaryAge();
            //List<string> data = WebHandler.ReadWebPage("http://www.urbandictionary.com/popular.php?character=A");
            //List<string> data = WebHandler.ReadWebPage("http://time.com/4373616/text-abbreviations-acronyms/");



            //CMain controller = new CMain();

        }
    }
}
