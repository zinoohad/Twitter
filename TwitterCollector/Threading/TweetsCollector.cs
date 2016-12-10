using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;

namespace TwitterCollector.Threading
{
    public class TweetsCollector : BaseThread
    {
        private DBHandler db = new DBHandler();
        private List<string> Keywords = new List<string>();
        public override void RunThread()
        {
            try
            {
                bool newSubject = bool.Parse(db.GetValueByKey("StartNewSubject").ToString());
                if (newSubject) StartNewSearch();
                else ContinueToSearch();

            }
            catch{}
        }
        private void StartNewSearch()
        {

        }
        private void ContinueToSearch()
        {
        }
    }
}
