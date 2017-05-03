using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Forms;
using TwitterCollector.Threading;

namespace TwitterCollector.Controllers
{
    public class CSettings : BaseController
    {
        private Settings form = new Settings();

        private Supervisor supervisor = new Supervisor();

        private bool startSupervisorAutomatically;

        public CSettings()
        {
            form.SetController(this);
            startSupervisorAutomatically  = db.GetValueByKey("StartSupervisorAutomatically", 0).ToString().Equals("1");
            if (startSupervisorAutomatically)
            {
                // TODO: Start all threads
                supervisor.Start();
                form.SupervisorTS_State(true);
            }
            else
            {
                form.SupervisorTS_State(false);
            }
            form.SetSupervisorIntervalsValue(int.Parse(db.GetValueByKey("SupervisorIntervals",30).ToString()));
        }

        public void AddThreadToViewTable()
        {

        }

        #region UI Functions

        public void ChangeThreadState(int processID, string threadState)
        {
            db.UpdateThreadDesirableState(processID, threadState);
        }

        public void ChangeStartSupervisorAutomaticallyState(bool state)
        {
            if(state)
                db.SetValueByKey("StartSupervisorAutomatically", "1");
            else
                db.SetValueByKey("StartSupervisorAutomatically", "0");
        }

        public void supervisiorTS_CheckedChanged(bool state)
        {
            if (state && !supervisor.IsAlive)
                supervisor.Start();
            else if (!state && supervisor.IsAlive)
            {
                supervisor.Abort();
                form.StopAllProcess();
                db.StopAllThreads(SupervisorThreadState.Stop);
            }
        }

        public void ChangeSupervisorIntervals(decimal value)
        {
            db.SetValueByKey("SupervisorIntervals", (int)value);
        }

        #endregion

        #region Functions

        public void ChangeUIThreadState(int processID, string threadName, int subjectID, SupervisorThreadState threadState)
        {
            if (!form.DGV.InvokeRequired)
                form.UpdateThreadState(processID, threadName, subjectID, Environment.MachineName, threadState);
            else
                form.DGV.Invoke(new MethodInvoker(() => { form.UpdateThreadState(processID, threadName, subjectID, Environment.MachineName, threadState); }));
        }

        public void LoadThreadsTable()
        {
            Global.Sleep(1);
            DataTable dt = db.GetTable("ViewThreadsControl", string.Format("MachineName = '{0}'", Environment.MachineName), "SubjectID DESC");
            dt.Columns.Remove("SubjectID");
            form.LoadGridFromDataTable(dt);
        }

        public void AbortSupervisor()
        {
            supervisor.Abort();
        }

        public void UpdateProcessID(string threadName, int subjectID, int processID)
        {
            if (!form.DGV.InvokeRequired)
                form.UpdateProcessID(threadName, processID, Environment.MachineName);
            else
                form.DGV.Invoke(new MethodInvoker(() => { form.UpdateProcessID(threadName, processID, Environment.MachineName); }));
            //form.UpdateProcessID(threadName, processID, Environment.MachineName);
        }

        #endregion

        #region Implement Methods

        public override Form GetUI()
        {
            return form;
        }

        public override void ToolStripAction(string buttonName)
        {
            Global.ToolStripAction(buttonName, this);
        }

        #endregion
    }
}
