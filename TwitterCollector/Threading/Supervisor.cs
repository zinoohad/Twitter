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
        private Dictionary<int, Dictionary<string, BaseThread>> subjectThreads = // <subjectID ,<threadName, Thread>>
            new Dictionary<int, Dictionary<string, BaseThread>>();
        public override void RunThread()
        {
            DataTable subjectsDT = db.GetActiveSubjects();
            foreach (DataRow subject in subjectsDT.Rows)
            {
                // Start TweetsCollector thread
                bool newSubject = bool.Parse(subject["StartNewSubject"].ToString());
                int subjectID = int.Parse(subject["ID"].ToString());
                AddThread(subjectID, new TweetsCollector(), subjectID, newSubject);
                
                //TODO: Add the rest threads

            }
        }
        private void AddThread(int subjectID, BaseThread thread, params object[] Params)
        {
            Type t = thread.GetType();
            string threadName = t.Name;
            if (subjectThreads.ContainsKey(subjectID))
            {
                if (subjectThreads[subjectID].ContainsKey(threadName))
                {
                    if (!subjectThreads[subjectID][threadName].IsAlive)
                    {
                        if (Params.Length != 0)
                            thread.SetInitialParams(Params);
                        subjectThreads[subjectID][threadName] = thread;
                        thread.Start();
                    }
                    // The thread already exists.
                    // TODO: Check thread status and update database...
                    return;
                }
                else
                {
                    subjectThreads[subjectID].Add(threadName, thread);
                    if (Params.Length != 0)
                        thread.SetInitialParams(Params);
                    thread.Start();
                }
            }
            else
            {
                Dictionary<string,BaseThread> localThread = new Dictionary<string,BaseThread>();
                localThread.Add(threadName,thread);
                subjectThreads.Add(subjectID, localThread);
                if (Params.Length != 0)
                    thread.SetInitialParams(Params);
                thread.Start();
            }
        }
    }
}
