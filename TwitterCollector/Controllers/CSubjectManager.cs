using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Forms;

namespace TwitterCollector.Controllers
{
    public class CSubjectManager : BaseController
    {
        #region Params
        private SubjectManager sm;
        #endregion
        #region UI Functions
        #endregion
        #region Functions
        #endregion
        public CSubjectManager(SubjectManager sm)
        {
            this.sm = sm;
            sm.SetController(this);
        }
        public void AddSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                MessageBox.Show("Can't create new empty subject.","ERROR");
            }
            else
            {
                //TODO: write to db
                sm.AddSubjectToGrid(0, subject);
            }
        }
        public void AddKeyword(string subject, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Can't create new empty keyword.","ERROR");
            }
            else
            {
                //TODO: write to db
                sm.AddKeywordToGrid(0, keyword);
            }
        }
        public void SetSubjects(Dictionary<int, string> subjects)
        {
        }
        public void SetKeywords(Dictionary<int, Dictionary<int, string>> keywords)
        {
        }
        public void RemoveSubject(string subject, int rowNumber)
        {
            sm.RemoveSubjectFromGrid(rowNumber);
        }
        public void RemoveKeyword(string subject, string keyword, int rowNumber)
        {
            sm.RemoveKeywordFromGrid(rowNumber);
        }
    }
}
