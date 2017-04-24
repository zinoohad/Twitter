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
            CreateAllThreads();
            while (ThreadOn)
            {
                try
                {
                    //TODO: Check thread statuses
                    Global.Sleep(30);

                    CheckStateChangesInThreadsState();

                    // Abort all threads if the disk space low.
                    try
                    {
                        if (Global.IsHardDriveSpaceLow)
                            this.Abort();
                    }
                    catch
                    {
                        this.Abort();
                    }

                    //while (true) Global.Sleep(1000);    //TODO: Remove this line
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
            }
        }

        #endregion

        #region Functions

        private void CreateAllThreads()
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

            Global.settings.LoadThreadsTable();
        }

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
                        //if (db.UpsertThreadWithLastStateReturn(threadName, subjectID, thread.ThreadProcessID) == SupervisorThreadState.Running)
                        //{
                        //    thread.Start();
                        //    db.UpdateThreadProcessID(threadName, subjectID, thread.ThreadProcessID);
                        //}
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
                    //if(db.UpsertThreadWithLastStateReturn(threadName, subjectID, thread.ThreadProcessID) == SupervisorThreadState.Running)
                    //{
                    //    thread.Start();
                    //    db.UpdateThreadProcessID(threadName, subjectID, thread.ThreadProcessID);
                    //}
                }
            }
            else  // Create subject thread pool and run the given thread
            {
                List<GeneralThreadParams> localThreads = new List<GeneralThreadParams>();
                localThreads.Add(new GeneralThreadParams(subjectID, thread));
                subjectThreads.Add(subjectID, localThreads); //Add the current thread to the new pool
                if (Params.Length != 0)
                    thread.SetInitialParams(Params);    //Send parameters to thread
                //if (db.UpsertThreadWithLastStateReturn(threadName, subjectID, thread.ThreadProcessID) == SupervisorThreadState.Running)
                //{
                //    thread.Start();
                //    db.UpdateThreadProcessID(threadName, subjectID, thread.ThreadProcessID);
                //}
            }
            if (db.UpsertThreadWithLastStateReturn(threadName, subjectID, thread.ThreadProcessID) == SupervisorThreadState.Running)
            {
                thread.Start();
                db.UpdateThreadProcessID(threadName, subjectID, thread.ThreadProcessID);
                //Global.settings.UpdateProcessID(threadName, subjectID, thread.ThreadProcessID);
            }
        }

        private void AbortAllThreads()
        {
            foreach (KeyValuePair<int, List<GeneralThreadParams>> thread in subjectThreads)
            {
                foreach (GeneralThreadParams threadParam in thread.Value)
                {
                    if(threadParam.Thread.IsAlive)
                        threadParam.Thread.Abort();
                }
            }
        }

        private void CheckStateChangesInThreadsState()
        {
            DataTable dt = db.GetTable("ThreadsControl", string.Format("MachineName = '{0}'", Environment.MachineName));
            if (dt == null || dt.Rows.Count == 0) 
                return;

            foreach(KeyValuePair<int, List<GeneralThreadParams>> t in subjectThreads)
            {
                List<GeneralThreadParams> lgtp = t.Value;
                int subjectID = t.Key;

                foreach(GeneralThreadParams gtp in lgtp)
                {
                    int threadID = gtp.Thread.ThreadProcessID;
                    DataRow[] drs = dt.Select(string.Format("ThreadName = '{0}' AND SubjectID = {1}", gtp.Name, subjectID));
                    //DataRow[] drs = dt.Select(string.Format("ThreadProcessID = {0} AND SubjectID = {1}", threadID, subjectID));
                    if(drs == null || drs.Length == 0)
                        continue;

                    if (!drs[0]["ThreadProcessID"].ToString().Equals(threadID.ToString()))
                        db.UpdateThreadProcessID(drs[0]["ThreadName"].ToString(), subjectID, threadID);

                    if (drs[0]["ThreadDesirableState"].ToString().Equals("Start") && gtp.Thread.ThreadState != System.Threading.ThreadState.Running)
                    {
                        // Start the thread
                        gtp.Thread.Start();
                        UpdateUIThreadState(gtp.Thread.ThreadProcessID, gtp.Name, subjectID, SupervisorThreadState.Running);
                    }
                    else if (drs[0]["ThreadDesirableState"].ToString().Equals("Stop") && gtp.Thread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        // Stop the thread
                        gtp.Thread.Abort();
                        UpdateUIThreadState(gtp.Thread.ThreadProcessID, gtp.Name, subjectID, SupervisorThreadState.Stop);
                    }
                }
            }
        }

        private void UpdateUIThreadState(int processID, string threadName, int subjectID, SupervisorThreadState threadState)
        {
            db.ChangeThreadState(processID, threadState);
            Global.settings.ChangeUIThreadState(processID, threadName, subjectID, threadState);
        }

        #endregion

        #region Thread Functions

        public override void Abort()
        {
            AbortAllThreads();
            base.Abort();
        }

        #endregion
    }
}
