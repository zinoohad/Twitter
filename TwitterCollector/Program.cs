using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using DataBaseConnections;

namespace TwitterCollector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DBHandler dbh = new DBHandler();
            //List<string> data = WebHandler.ReadWebPage("http://www.urbandictionary.com/popular.php?character=A");
            List<string> data = WebHandler.ReadWebPage("http://time.com/4373616/text-abbreviations-acronyms/");
        }
    }
}
