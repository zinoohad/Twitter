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
    public partial class SubjectManager : Form, FormBase
    {
        #region Params
        private CSubjectManager controller;
        #endregion
        public SubjectManager()
        {
            InitializeComponent();
        }

        #region Handlers
        private void addSubjectB_Click(object sender, EventArgs e)
        {
            controller.AddSubject(addSubjectTB.Text);
            addSubjectTB.Text = "";
        }
        private void addKeywordB_Click(object sender, EventArgs e)
        {
            controller.AddKeyword(addKeywordTB.Text);
            addKeywordTB.Text = "";
        }
        private void subjectDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                DataGridViewRow selectedRow = dgvSubject.Rows[e.RowIndex];
                string CurrentSubject = (string)selectedRow.Cells[0].Value;
                controller.RemoveSubject(CurrentSubject,e.RowIndex);

                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                //dgvSubject.Rows.RemoveAt(e.RowIndex);
            }
            else if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow selectedRow = dgvSubject.Rows[e.RowIndex];
                string CurrentSubject = (string)selectedRow.Cells[0].Value;
                controller.SetSelectedSubject(CurrentSubject);
            }
        }
        private void keywordDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow selectedRow = dgvKeyword.Rows[e.RowIndex];
                controller.RemoveKeyword((string)selectedRow.Cells[0].Value, e.RowIndex);
                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                //dgvKeyword.Rows.RemoveAt(e.RowIndex);
            }
            else if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
            }
        }
        private void Subject_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addSubjectB_Click(new object(), new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        private void Keyword_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addKeywordB_Click(new object(), new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        private void toolStripAction_Click(object sender, EventArgs e)
        {
            controller.ToolStripAction(((ToolStripButton)sender).Name);
        }
        private void onExit_Click(object sender, FormClosingEventArgs e)
        {
            Global.ExitApplication(sender, e);
        }
        #endregion
        #region Interface Implement
        public void SetController(BaseController controller)
        {
            this.controller = (CSubjectManager)controller;
        }
        #endregion
        #region Functions
        public void LoadSubjects(Dictionary<int, string> subjects)
        {
            foreach (KeyValuePair<int, string> subject in subjects)
                AddSubjectToGrid(subject.Value);
            dgvSubject.Rows[1].Cells[1].Selected = false;
        }
        public void LoadKeywords(Dictionary<int, string> keywords)
        {
            dgvKeyword.Rows.Clear();
            foreach (KeyValuePair<int, string> keyword in keywords)
                AddKeywordToGrid(keyword.Value);
        }
        public void AddSubjectToGrid(string subject)
        {
            dgvSubject.Rows.Add(subject);
        }
        public void AddKeywordToGrid(string keyword)
        {
            dgvKeyword.Rows.Add(keyword);
        }
        public void RemoveSubjectFromGrid(int rowNumber)
        {
            dgvSubject.Rows.RemoveAt(rowNumber);
        }
        public void RemoveKeywordFromGrid(int rowNumber)
        {
            dgvKeyword.Rows.RemoveAt(rowNumber);
        }
        #endregion

        


    }
}
