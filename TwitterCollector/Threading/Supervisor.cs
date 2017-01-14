using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Threading
{
    public class Supervisor : BaseThread
    {
        #region Params
        private Dictionary<int, Dictionary<string, BaseThread>> subjectThreads = // <subjectID ,<threadName, Thread>>
            new Dictionary<int, Dictionary<string, BaseThread>>();
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
                if (subjectThreads[subjectID].ContainsKey(threadName))  // Check if the thread already exists in the subject pool
                {
                    if (!subjectThreads[subjectID][threadName].IsAlive) // Check if the thread is alive
                    {
                        if (Params.Length != 0)
                            thread.SetInitialParams(Params); //Send parameters to thread
                        subjectThreads[subjectID][threadName] = thread; // Save thread
                        thread.Start();
                    }
                    // The thread already exists.
                    // TODO: Check thread status and update database...
                    return;
                }
                else
                {
                    subjectThreads[subjectID].Add(threadName, thread);  // Create the thread for this subject and run it.
                    if (Params.Length != 0)
                        thread.SetInitialParams(Params);    //Send parameters to thread
                    thread.Start();
                }
            }
            else  // Create subject thread pool
            {
                Dictionary<string,BaseThread> localThread = new Dictionary<string,BaseThread>();
                localThread.Add(threadName,thread);
                subjectThreads.Add(subjectID, localThread); //Add the current thread to the new pool
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
