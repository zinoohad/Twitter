using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Forms;
using TwitterCollector.Common;

namespace TwitterCollector.Controllers
{
    public class CSubjectManager : BaseController
    {
        #region Params
        private SubjectManager sm;
        private Dictionary<int, Dictionary<int, string>> keywords = new Dictionary<int,Dictionary<int,string>>();
        private Dictionary<int, string> subjects = new Dictionary<int,string>();
        #endregion
        #region UI Functions
        #endregion
        #region Functions
        private int CheckIfValueExistsInDictionary(Dictionary<int, string> dic, string value)
        {
            foreach(KeyValuePair<int, string> d in dic) // or foreach(book b in books.Values)
            {
                if (d.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    return d.Key;
            }
            return -1;
        }
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
                if (subjects.Values.Contains(subject)) return;  // The subject already exists
                int subjectID = db.AddRemoveSubject(Common.Action.ADD, 0, subject); //Save subject in DB
                subjects.Add(subjectID, subject);   //Save subject in global param
                int keywordID = db.AddRemoveKeyword(Common.Action.ADD, subjectID, 0, subject);  //Save keyword in DB
                //Save keyword in global param
                Dictionary<int, string> tmpKeyword = new Dictionary<int, string>();
                tmpKeyword.Add(keywordID, subject);
                keywords.Add(subjectID, tmpKeyword);

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
                int subjectID = CheckIfValueExistsInDictionary(subjects,subject);
                if (keywords[subjectID].Values.Contains(keyword)) return;    // The keyword already exists
                int keywordID = db.AddRemoveKeyword(Common.Action.ADD, subjectID, 0, keyword); //Save keyword in DB

                //Save keyword in global param
                Dictionary<int, string> tmpKeyword = keywords[subjectID];
                tmpKeyword.Add(keywordID, keyword);
                keywords[subjectID] = tmpKeyword;

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
