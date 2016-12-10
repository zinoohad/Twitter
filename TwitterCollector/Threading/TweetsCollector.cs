using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using Twitter;
using Twitter.Classes;

namespace TwitterCollector.Threading
{
    public class TweetsCollector : BaseThread
    {
        #region Params
        private DBHandler db = new DBHandler();
        private TwitterAPI twitter = new TwitterAPI();
        private List<string> Keywords = new List<string>();
        #endregion
        public override void RunThread()
        {
            try
            {
                bool newSubject = bool.Parse(db.GetValueByKey("StartNewSubject").ToString());
                Keywords = db.GetActiveSubjects();
                if (newSubject) StartNewSearch();
                else ContinueToSearch();

            }
            catch (Exception e)
            {
                new TwitterException(e);
            }
        }
        /// <summary>
        /// New search need to start.
        /// It's possible there a new subjects to check.
        /// </summary>
        private void StartNewSearch()
        {
            db.SetValueByKey("StartNewSubject", false); //  Set flag to false


        }
        private void ContinueToSearch()
        {
        }

        private void SaveTweets(List<Tweets> tweets)
        {

        }
    }
}
