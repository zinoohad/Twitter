using System;
using System.Collections.Generic;
using System.Data;
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
