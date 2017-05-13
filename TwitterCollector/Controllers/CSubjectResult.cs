using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Forms;

namespace TwitterCollector.Controllers
{
    public class CSubjectResult : BaseController
    {
        #region Params
        private SubjectResult form = new SubjectResult();
        #endregion
        public CSubjectResult()
        {
            form.SetController(this);
            LoadSubjects();
            //form.ShowDialog();
        }
        #region UI Functions
        public void SubjectChanged(string subjectName, int subjectLineNumber)
        {
        }
        public void SwitchStateChanged(SwitchState state)
        {
        }
        public void OpenDiagrams()
        {
            if (!form.XmappisRunning)
            {
                string xmapp = @"C:\xampp\xampp_start";
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = xmapp;
                Process.Start(startInfo);
                form.XmappisRunning = true;
            }
            string path = @"http://localhost/Twitter1/index.html";
            Process.Start("chrome.exe", path);
        }
        #endregion
        #region Functions
        private void LoadSubjects()
        {
            List<string> subjects = new List<string>();
            var subjectsDT = db.GetActiveSubjects();
            foreach(DataRow dt in subjectsDT.Rows)
                subjects.Add(dt["Subject"].ToString());
            form.LoadSubjectComboBox(subjects);
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
    public enum SwitchState
    {
        ON,
        OFF
    }
}
