using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using TwitterCollector.Objects;

namespace TwitterCollector.Threading
{
    public class Supervisor : BaseThread
    {
        #region Params

        private Dictionary<int, List<GeneralThreadParams>> subjectThreads = new Dictionary<int, List<GeneralThreadParams>>();

        #endregion

        #region Overriding

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    DataTable subjectsDT = db.GetActiveSubjects(true);

                    // Threads for specific subject
                    foreach (DataRow subject in subjectsDT.Rows)
                    {
                        // Start TweetsCollector thread
                        bool newSubject = bool.Parse(subject["StartNewSubject"].ToString());
                        int subjectID = int.Parse(subject["ID"].ToString());
                        AddThread(subjectID, new TweetsCollector(), subjectID, newSubject);

                        // Start User Collector thread
                        AddThread(subjectID, new UsersCollector(), subjectID);

                        //TODO: Add the rest threads

                    }

                    // Threads that common to all subjects

                    // Start User TweetsPosNeg thread
                    AddThread(0, new TweetsPosNeg());

                    // Start User SentimentAnalysis thread
                    AddThread(0, new SentimentAnalysis());

                    //Start ImageAnalysis thread
                    AddThread(0, new ImageAnalysis());





                    // Abort all threads if the disk space low.
                    try
                    {
                        if (Global.IsHardDriveSpaceLow)
                            AbortAllThreads();
                    }
                    catch
                    {
                        AbortAllThreads();
                    }

                    while (true) System.Threading.Thread.Sleep(1000000);    //TODO: Remove this line
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }
        }

        #endregion

        #region Functions

        private void AddThread(int subjectID, BaseThread thread, params object[] Params)
        {
            
            Type t = thread.GetType();
            string threadName = t.Name;
            if (subjectThreads.ContainsKey(subjectID))  // Check if was create thread pool for the subjectID
            {
                GeneralThreadParams[] tmpThread = subjectThreads[subjectID].Where(x => x.Name.Equals(threadName)).ToArray();
                if (tmpThread.Length > 0)  // Check if the thread already exists in the subject pool
                {
                    if (!tmpThread[0].Thread.IsAlive) // Check if the thread is alive
                    {
                        if (Params.Length != 0)
                            tmpThread[0].Thread.SetInitialParams(Params); //Send parameters to thread
                        tmpThread[0].SetBaseThread(thread); // Save thread
                        thread.Start();
                    }
                    // The thread already exists.
                    // TODO: Check thread status and update database...
                    return;
                }
                else
                {
                    subjectThreads[subjectID].Add(new GeneralThreadParams(subjectID,thread));  // Create the thread for this subject and run it.
                    if (Params.Length != 0)
                        thread.SetInitialParams(Params);    //Send parameters to thread
                    thread.Start();
                    db.UpsertThread(threadName, subjectID, thread.ThreadProcessID);
                }
            }
            else  // Create subject thread pool and rub the given thread
            {
                List<GeneralThreadParams> localThreads = new List<GeneralThreadParams>();
                localThreads.Add(new GeneralThreadParams(subjectID, thread));
                subjectThreads.Add(subjectID, localThreads); //Add the current thread to the new pool
                if (Params.Length != 0)
                    thread.SetInitialParams(Params);    //Send parameters to thread
                thread.Start();
                db.UpsertThread(threadName, subjectID, thread.ThreadProcessID);
            }
        }

        private void AbortAllThreads()
        {
            foreach (KeyValuePair<int, List<GeneralThreadParams>> thread in subjectThreads)
            {
                foreach (GeneralThreadParams threadParam in thread.Value)
                {
                    threadParam.Thread.Abort();
                }
            }
            Abort();
        }

        private void CheckStateChangesInThreadsState()
        {
            DataTable dt = db.GetTable("ThreadsControl", string.Format("MachineName = '{0}'", Environment.MachineName));
            if (dt == null || dt.Rows.Count == 0) 
                return;

            foreach (DataRow dr in dt.Rows)
            {

            }
            //TODO: COMPLETE THIS FUNCTION
        }

        #endregion

        #region Thread Functions
        public override void Abort()
        {
            //TODO: STOP ALL THREADS
            base.Abort();
        }
        #endregion
    }
}
