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

            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                DataGridViewRow selectedRow = dgvSubject.Rows[e.RowIndex];
                string CurrentSubject = (string)selectedRow.Cells[0].Value;
                controller.RemoveSubject(CurrentSubject,e.RowIndex);

                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                //dgvSubject.Rows.RemoveAt(e.RowIndex);
            }
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                //DataGridViewRow selectedRow = dgvSubject.Rows[e.RowIndex];
                //string CurrentSubject = (string)selectedRow.Cells[0].Value;
                //string lang = (string)((DataGridViewComboBoxCell)selectedRow.Cells[1]).EditedFormattedValue;
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
            if (e.ColumnIndex == 2)
            {
                DataGridViewRow selectedRow = dgvKeyword.Rows[e.RowIndex];
                controller.RemoveKeyword((string)selectedRow.Cells[0].Value, e.RowIndex);
                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                //dgvKeyword.Rows.RemoveAt(e.RowIndex);
            }
            else if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
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
        private void SubjectManager_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'twitterDataSet.Languages' table. You can move, or remove it, as needed.
            this.languagesTableAdapter.Fill(this.twitterDataSet.Languages);

        }

        private void dgvSubject_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        /// <summary>
        /// Evant calls when chackBox value changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSubject_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView ldgv = (DataGridView)sender;
            Point CellAddress = ldgv.CurrentCellAddress;

            if (CellAddress.X == 1 && CellAddress.Y != -1)
            {
                DataGridViewRow selectedRow = dgvSubject.Rows[CellAddress.Y];
                string CurrentSubject = (string)selectedRow.Cells[0].Value;
                string lang = (string)((DataGridViewComboBoxCell)selectedRow.Cells[1]).EditedFormattedValue;
                controller.UpdateSubjectLanguage(CurrentSubject, lang); 
            }
        }
        private void dgvKeyword_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView ldgv = (DataGridView)sender;
            Point CellAddress = ldgv.CurrentCellAddress;

            if (CellAddress.X == 1 && CellAddress.Y != -1)
            {
                DataGridViewRow selectedRow = dgvKeyword.Rows[CellAddress.Y];
                string CurrentKeyword = (string)selectedRow.Cells[0].Value;
                string lang = (string)((DataGridViewComboBoxCell)selectedRow.Cells[1]).EditedFormattedValue;
                controller.UpdateKeywordLanguage(CurrentKeyword, lang);
            }
        }
        #endregion

        #region Interface Implement
        public void SetController(BaseController controller)
        {
            this.controller = (CSubjectManager)controller;
        }
        #endregion

        #region Functions

        public void LoadSubjects(List<SubjectO> subjects)
        {
            foreach (SubjectO s in subjects)
            {
                AddSubjectToGrid(s.Name, s.LanguageName);
            }
        }

        public void LoadKeywords(List<KeywordO> keywords)
        {
            dgvKeyword.Rows.Clear();
            foreach (KeywordO k in keywords)
                AddKeywordToGrid(k.Name, k.LanguageName);
        }

        public void AddSubjectToGrid(string subject, string lang = null)
        {
            int row;
            if(lang != null)
                row = dgvSubject.Rows.Add(subject, lang);
            else
                row = dgvSubject.Rows.Add(subject, "English");
        }

        public void AddKeywordToGrid(string keyword, string lang = null)
        {
            if (lang != null)
                dgvKeyword.Rows.Add(keyword, lang);
            else
                dgvKeyword.Rows.Add(keyword, "English");
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
