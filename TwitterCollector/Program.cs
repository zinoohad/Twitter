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
using TwitterCollector.Threading;
using System.IO;
using TopicSentimentAnalysis.Classes;

namespace TwitterCollector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new CMain();
            Application.Run();
            //var db = Global.DB;
            //DataTable dt = db.GetTable("DictionaryAge","WORD NOT IN ( SELECT Word FROM DictionaryAllAges )");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    WordAge word = WordSentimentAnalysis.CheckWordAge(dr["Word"].ToString())[0];
            //    bool isEmoticon = bool.Parse(dr["IsEmoticon"].ToString());
            //    db.UpsertDictionaryAllAges(word, isEmoticon);
            //}


        }
    }
}

