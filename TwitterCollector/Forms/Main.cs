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
    public partial class Main : Form, FormBase
    {
        #region Params

        private CMain controller;

        private SubjectResultUI _mainFormValues;

        #endregion

        public Main()
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

        private void subjectListLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubjectSelectionChanged(subjectListLB.SelectedItem.ToString());
        }
        #endregion

        #region Interface Implement

        public void SetController(BaseController controller)
        {
            this.controller = (CMain)controller;
        }
        #endregion

        #region Functions
        public void LoadLastSubjectResults(SubjectResultUI values)
        {
            _mainFormValues = values;
            // Load subject list box
            foreach (string s in values.Subjects)
                subjectListLB.Items.Add(s);
            if (values.Subjects.Count > 0)
                subjectListLB.SelectedIndex = 0;
        }

        public void SubjectSelectionChanged(string subject)
        {
            lastSubject.Text = subject;
            this.keyWordsChart.Series[0].Points.Clear();
            foreach (KeywordO k in _mainFormValues.SubjectKeywordsTweetCount[subject])
                this.keyWordsChart.Series["Tweet Count"].Points.AddXY(k.Name, k.RelatedTweetsCount);
        }

        public void ReloadSubjectResults(SubjectResultUI values)
        {
            _mainFormValues = values;
            if(subjectListLB.InvokeRequired)
            {
                subjectListLB.Invoke(new MethodInvoker(() => { ReloadUiControllers(values); }));
            }
            else
            {
                ReloadUiControllers(values);
            }
        }

        private void ReloadUiControllers(SubjectResultUI values)
        {
            string selectedItem = null;
            if (subjectListLB.Items.Count > 0)
            {
                // Save the current chosen subject
                selectedItem = subjectListLB.SelectedItem.ToString();
            }

            // Update also the subject list
            if (subjectListLB.Items.Count != values.Subjects.Count)
            {                
                subjectListLB.Items.Clear();
                if(values.Subjects.Count == 0)
                {
                    lastSubject.Text = "No Subject To Display";
                    this.keyWordsChart.Series[0].Points.Clear();
                    return;
                }
                foreach (string s in values.Subjects)
                    subjectListLB.Items.Add(s);
                if (selectedItem != null && subjectListLB.Items.Contains(selectedItem))
                    subjectListLB.SelectedItem = selectedItem;
                else
                    subjectListLB.SelectedIndex = 0;
            }
            // Update only the chart
            else if (subjectListLB.Items.Count > 0)
            {
                SubjectSelectionChanged(selectedItem);
            }
        }
        #endregion


    }
}
