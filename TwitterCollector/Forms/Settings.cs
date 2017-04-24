using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Controllers;

namespace TwitterCollector.Forms
{
    public partial class Settings : Form, FormBase
    {

        #region Params

        private CSettings controller;

        public DataGridView DGV { get { return threadDGV; } }

        #endregion

        public Settings()
        {
            InitializeComponent();
        }

        #region Handlers

        private void toolStripAction_Click(object sender, EventArgs e)
        {
            controller.ToolStripAction(((ToolStripButton)sender).Name);
        }

        private void onExit_Click(object sender, FormClosingEventArgs e)
        {
            Global.ExitApplication(sender, e);
        }

        private void dgvThreads_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView ldgv = (DataGridView)sender;
            Point CellAddress = ldgv.CurrentCellAddress;

            if (!ldgv.IsCurrentCellDirty)
            {
                if (CellAddress.X == 4 && CellAddress.Y != -1)
                {
                    DataGridViewRow selectedRow = ldgv.Rows[CellAddress.Y];
                    string desirableState = (string)selectedRow.Cells["ThreadDesirableState"].Value;
                    //Console.WriteLine(desirableState + "-" + ldgv.IsCurrentCellDirty);
                    int processID = int.Parse(selectedRow.Cells["ThreadProcessID"].EditedFormattedValue.ToString());

                    switch (desirableState)
                    {
                        case "Start":
                            selectedRow.Cells["ThreadState"].Value = "Starting";
                            selectedRow.Cells["ThreadState"].Style = Orange;
                            controller.ChangeThreadState(processID, desirableState);
                            break;
                        case "Stop":
                            selectedRow.Cells["ThreadState"].Value = "Aborting";
                            selectedRow.Cells["ThreadState"].Style = Orange;
                            controller.ChangeThreadState(processID, SupervisorThreadState.Stop.ToString());
                            break;
                    }
                }
            }
        }

        private void startOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            controller.ChangeStartSupervisorAutomaticallyState(startOnStartup.Checked);
        }

        private void supervisiorTS_CheckedChanged(object sender, EventArgs e)
        {
            controller.supervisiorTS_CheckedChanged(supervisiorTS.Checked);
        }

        private void threadDGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

        #region Styles

        private DataGridViewCellStyle Green 
        { 
            get 
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.Green;
                style.ForeColor = Color.White;
                return style;        
            } 
        }

        private DataGridViewCellStyle Red
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.Red;
                style.ForeColor = Color.White;
                return style;
            }
        }

        private DataGridViewCellStyle Orange
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.Orange;
                style.ForeColor = Color.Black;
                return style;
            }
        }

        #endregion

        #region Interface Implement

        public void SetController(BaseController controller)
        {
            this.controller = (CSettings)controller;
        }

        #endregion

        #region Functions

        public void UpsertRowToGrid(int id, string threadName,string subjectName, string threadState, string desirableState, int processID, string machineName)
        {
            int rowIndex = -1;

            DataGridViewRow row = threadDGV.Rows
            .Cast<DataGridViewRow>()
            .Where(r => (r.Cells["ThreadName"].Value.ToString().Equals(threadName) &&
                r.Cells["SubjectName"].Value.ToString().Equals(subjectName) &&
                r.Cells["MachineName"].Value.ToString().Equals(machineName)))
            .First();

            rowIndex = row.Index;

            if (rowIndex == -1)
            {
                // Insert new row
                threadDGV.Rows.Add(id, threadName, subjectName, threadState, desirableState, processID, machineName);
            }
            else
            {
                // Change exists row
                threadDGV.Rows[rowIndex].Cells["ThreadState"].Value = threadState;
                threadDGV.Rows[rowIndex].Cells["ThreadProcessID"].Value = processID;
                threadDGV.Rows[rowIndex].Cells["ID"].Value = id;
            }
        }

        public void UpdateThreadState(int processID, string threadName, int subjectID, string machineName, SupervisorThreadState threadState)
        {
            int rowIndex = -1;

            DataGridViewRow row = threadDGV.Rows
            .Cast<DataGridViewRow>()
            .Where(r => (r.Cells["ThreadName"].Value.ToString().Equals(threadName) &&
                r.Cells["MachineName"].Value.ToString().Equals(machineName)))
            .First();

            rowIndex = row.Index;

            if (rowIndex == -1)
            {
                // Row not exists
                
            }
            else
            {
                // Change row state

                DataGridViewRow selectedRow = threadDGV.Rows[rowIndex];
                if (threadState == SupervisorThreadState.Running)
                {
                    selectedRow.Cells["ThreadState"].Value = "Running";
                    selectedRow.Cells["ThreadState"].Style = Green;
                }
                else if (threadState == SupervisorThreadState.Stop)
                {
                    selectedRow.Cells["ThreadState"].Value = "Stoped";
                    selectedRow.Cells["ThreadState"].Style = Red;
                }
                selectedRow.Cells["ThreadProcessID"].Value = processID;
                threadDGV.Rows[rowIndex].Cells["ThreadState"].Value = threadState.ToString();
            }
        }

        public void LoadGridFromDataTable(DataTable dt)
        {
            if (!threadDGV.InvokeRequired)
                threadDGV.Rows.Clear();     // Work on startup create in the same thread
            else
                threadDGV.Invoke(new MethodInvoker(() => { threadDGV.Rows.Clear(); }));     // Not work on startup, load in another thread.

            foreach (DataRow dr in dt.Rows)
            {
                if (!threadDGV.InvokeRequired)
                    AddSafeRowToGrid(dr);   // Work on startup create in the same thread
                else
                    threadDGV.Invoke(new MethodInvoker(() => { AddSafeRowToGrid(dr); }));     // Not work on startup, load in another thread.
            }
        }

        public void AddSafeRowToGrid(DataRow dr)
        {
            int rowIndex = threadDGV.Rows.Add(dr["ID"], dr["ThreadName"], dr["Subject"], dr["ThreadState"], dr["ThreadDesirableState"], dr["ThreadProcessID"], dr["MachineName"]);
            DataGridViewRow selectedRow = threadDGV.Rows[rowIndex];
            if (dr["ThreadDesirableState"].ToString().Equals("Stop"))
            {
                selectedRow.Cells["ThreadState"].Style = Red;
            }
            else
            {
                selectedRow.Cells["ThreadState"].Style = Orange;
                selectedRow.Cells["ThreadState"].Value = "Starting";
            }
        }

        public void SupervisorTS_State(bool state)
        {
            startOnStartup.Checked = supervisiorTS.Checked = state;
        }

        public void UpdateProcessID(string threadName, int processID, string machineName)
        {
            DataGridViewRow row = threadDGV.Rows
            .Cast<DataGridViewRow>()
            .Where(r => (r.Cells["ThreadName"].Value.ToString().Equals(threadName) &&
                r.Cells["MachineName"].Value.ToString().Equals(machineName)))
            .First();

            int rowIndex = row.Index;

            if (rowIndex != -1)
            {
                threadDGV.Rows[rowIndex].Cells["ThreadProcessID"].Value = processID;
            }           
        }

        #endregion

        





    }
}
