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

            if (CellAddress.X == 1 && CellAddress.Y != -1)
            {
                DataGridViewRow selectedRow = threadDGV.Rows[CellAddress.Y];
                string desirableState = (string)selectedRow.Cells["ThreadDesirableState"].Value;
                int id = int.Parse(((DataGridViewComboBoxCell)selectedRow.Cells["ID"]).EditedFormattedValue.ToString());

                System.Threading.ThreadState ts = System.Threading.ThreadState.Running;
                switch(desirableState)
                {
                    case "Start":
                        ((DataGridViewComboBoxCell)selectedRow.Cells["ThreadState"]).Value = "Starting";
                        ((DataGridViewComboBoxCell)selectedRow.Cells["ThreadState"]).Style = Orange;
                        ts = System.Threading.ThreadState.Running;
                        break;
                    case "Abort":
                        ((DataGridViewComboBoxCell)selectedRow.Cells["ThreadState"]).Value = "Aborting";
                        ((DataGridViewComboBoxCell)selectedRow.Cells["ThreadState"]).Style = Orange;
                        ts = System.Threading.ThreadState.StopRequested;
                        break;
                }

                controller.ChangeThreadState(id, ts);
            }
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

        #endregion

    }
}
