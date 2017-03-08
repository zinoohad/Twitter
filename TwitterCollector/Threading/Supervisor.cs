using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DataTable subjectsDT = db.GetActiveSubjects(true);
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
            while (true) System.Threading.Thread.Sleep(1000000);
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
                }
            }
            else  // Create subject thread pool
            {
                List<GeneralThreadParams> localThreads = new List<GeneralThreadParams>();
                localThreads.Add(new GeneralThreadParams(subjectID, thread));
                subjectThreads.Add(subjectID, localThreads); //Add the current thread to the new pool
                if (Params.Length != 0)
                    thread.SetInitialParams(Params);    //Send parameters to thread
                thread.Start();
            }
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
