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
using TwitterCollector.Objects;

namespace TwitterCollector.Forms
{
    public partial class SubjectResult : Form, FormBase
    {
        #region Params
        private CSubjectResult controller;
        #endregion
        public SubjectResult()
        {
            InitializeComponent();
        }
        #region Handlers
        private void toolStripAction_Click(object sender, EventArgs e)
        {
            controller.ToolStripAction(((ToolStripButton)sender).Name);
        }
        private void onOffS_CheckedChanged(object sender, EventArgs e)
        {
            if (onOffS.Checked)
                controller.SwitchStateChanged(SwitchState.ON);
            else controller.SwitchStateChanged(SwitchState.OFF);
        }

        private void subjectCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SubjectChanged(subjectCB.SelectedItem.ToString(), subjectCB.SelectedIndex);
        }
        private void onExit_Click(object sender, FormClosingEventArgs e)
        {
            Global.ExitApplication(sender, e);
        } 
        #endregion

        #region Realtime Updating UI
        public void IncrementTweets()
        {
        }
        public void IncrementUsers()
        {
        }
        #endregion
        #region Interface Implement
        public void SetController(BaseController controller)
        {
            this.controller = (CSubjectResult)controller;
        }
        #endregion
        #region Functions
        public void LoadSubjectComboBox(List<string> subjects)
        {
            subjectCB.Items.Add("Select Subject");
            subjectCB.SelectedIndex = 0;
            subjectCB.Items.AddRange(subjects.ToArray());
        }
        public void LoadAllParams(SubjectResultUI values)
        {
        }
        #endregion


    }
}
