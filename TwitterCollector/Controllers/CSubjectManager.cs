using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Forms;
using TwitterCollector.Common;
using System.Data;

namespace TwitterCollector.Controllers
{
    public class CSubjectManager : BaseController
    {
        #region Params
        private SubjectManager form = new SubjectManager();
        private Dictionary<int, Dictionary<int, string>> keywords = new Dictionary<int,Dictionary<int,string>>();
        private Dictionary<int, string> subjects = new Dictionary<int,string>();
        private int CurrentSubjectID = -1;
        #endregion
        public CSubjectManager()
        {
            form.SetController(this);
            LoadSubjectsAndKeywords();
            //form.ShowDialog();
        }
        #region UI Functions
        public void AddSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                MessageBox.Show("Can't create new empty subject.", "ERROR");
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

                form.AddSubjectToGrid(subject);
            }
        }
        public void AddKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                Global.ShowDialogMessageOk("Can't create new empty keyword.");
                return;
            }
            else if (CurrentSubjectID == -1)
            {
                Global.ShowDialogMessageOk("Please select subject first.");
                return;
            }
            else
            {
                //int subjectID = CheckIfValueExistsInDictionary(subjects, subject);
                if (keywords[CurrentSubjectID].Values.Contains(keyword)) return;    // The keyword already exists
                int keywordID = db.AddRemoveKeyword(Common.Action.ADD, CurrentSubjectID, 0, keyword); //Save keyword in DB

                //Save keyword in global param
                Dictionary<int, string> tmpKeyword = keywords[CurrentSubjectID];
                tmpKeyword.Add(keywordID, keyword);
                keywords[CurrentSubjectID] = tmpKeyword;

                form.AddKeywordToGrid(keyword);
            }
        }
        public void RemoveSubject(string subject, int rowNumber)
        {
            int subjectID = subjects.Where(s => s.Value.Equals(subject)).Select(s => s.Key).ToArray()[0];
            if (keywords[subjectID].Count > 0)  //Have keywords
            {
                if (!Global.ShowDialogMessageYesNo("Are you sure you want delete this subject with all his keywords?")) return;  //Do not delete this subject 
            }
            foreach (KeyValuePair<int, string> keyword in keywords[subjectID])  //Delete all keywords
            {
                db.AddRemoveKeyword(Common.Action.REMOVE, subjectID, keyword.Key); //Remove keyword from DB
                form.RemoveKeywordFromGrid(0);
            }
            db.AddRemoveSubject(Common.Action.REMOVE, subjectID);   //Remove subject from db
            form.RemoveSubjectFromGrid(rowNumber);    //Remove subject from UI
            CurrentSubjectID = -1;
        }
        public void RemoveKeyword(string keyword, int rowNumber)
        {
            Dictionary<int, string> tmpKeyword = keywords[CurrentSubjectID];
            int keywordID = tmpKeyword.Where(k => k.Value.Equals(keyword)).Select(k => k.Key).ToArray()[0]; //Get the key of the keyword from dictionary
            db.AddRemoveKeyword(Common.Action.REMOVE, CurrentSubjectID, keywordID); //Remove keyword from DB
            keywords[CurrentSubjectID].Remove(keywordID);   //Remove keyword from dictionary
            form.RemoveKeywordFromGrid(rowNumber);
        }
        private void LoadSubjectsAndKeywords()
        {
            DataTable subjectsDT = db.GetActiveSubjects(true);
            foreach (DataRow dr in subjectsDT.Rows)    // Run on all subjects
            {
                subjects.Add(int.Parse(dr["ID"].ToString()), dr["Subject"].ToString()); // Add subject to dictionary
                var subjectKeywords = db.GetSubjectKeywords((int)dr["ID"]); //Get subject keywords
                keywords.Add(int.Parse(dr["ID"].ToString()), subjectKeywords);  // Save subject keywords in global object
            }
            form.LoadSubjects(subjects);
        }
        public void SetSelectedSubject(string subjectName)
        {
            var keywordID = subjects.Where(sn => sn.Value.Equals(subjectName)).Select(sn => sn.Key).ToArray();
            if (keywordID.Length != 0)
            {
                CurrentSubjectID = keywordID[0];
                form.LoadKeywords(keywords[CurrentSubjectID]);
            }

        }
        #endregion
        #region Functions
        private int CheckIfValueExistsInDictionary(Dictionary<int, string> dic, string value)
        {
            foreach(KeyValuePair<int, string> d in dic) // 
            {
                if (d.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    return d.Key;
            }
            return -1;
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
}
