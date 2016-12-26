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

namespace TwitterCollector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            SubjectManager sm = new SubjectManager();
            sm.Visible = false;
            CSubjectManager controller = new CSubjectManager(sm);
            sm.ShowDialog();


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SubjectManager());
            //DBHandler dbh = new DBHandler();
            //List<string> data = WebHandler.ReadWebPage("http://www.urbandictionary.com/popular.php?character=A");
            //List<string> data = WebHandler.ReadWebPage("http://time.com/4373616/text-abbreviations-acronyms/");
        }
    }
}
