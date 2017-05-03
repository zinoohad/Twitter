using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Forms;
using TwitterCollector.Common;
using System.Data;
using TwitterCollector.Objects;

namespace TwitterCollector.Controllers
{
    public class CSubjectManager : BaseController
    {
        #region Params
        private SubjectManager form = new SubjectManager();
        private List<SubjectO> subjectsList = new List<SubjectO>();
        private SubjectO CurrentSubject;
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
                SubjectO s;
                if (subjectsList.Where(sn => sn.Name.ToLower().Equals(subject.ToLower())).ToList().Count > 0) return;    // The subject already exists
                int subjectID = db.AddRemoveSubject(Common.Action.ADD, 0, subject); //Save subject in DB
                subjectsList.Add(s = new SubjectO(subjectID, subject));   //Save subject in global param
                int keywordID = db.AddRemoveKeyword(Common.Action.ADD, subjectID, 0, subject);  //Save keyword in DB
                s.Keywords.Add(new KeywordO(keywordID, subject));
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
            else if (CurrentSubject == null)
            {
                Global.ShowDialogMessageOk("Please select subject first.");
                return;
            }
            else
            {
                if (CurrentSubject.Keywords.Where(k => k.Name.ToLower().Equals(keyword.ToLower())).ToList().Count > 0) return;   // The keyword already exists
                int keywordID = db.AddRemoveKeyword(Common.Action.ADD, CurrentSubject.ID, 0, keyword,CurrentSubject.LanguageID); //Save keyword in DB
                CurrentSubject.Keywords.Add(new KeywordO(keywordID, keyword));
                form.AddKeywordToGrid(keyword,CurrentSubject.LanguageName);
            }
        }

        public void UpdateKeywordLanguage(string keywordName, string Language)
        {
            var key = CurrentSubject.Keywords.Where(k => k.Name.Equals(keywordName)).Select(k => k).ToArray()[0]; //Get the key of the keyword
            if (db.UpdateKeywordLanguage(ref key, Language))
            {
                //The record was updated
            }
            else
            {
                //Failed to update record
            }
        }
        public void UpdateSubjectLanguage(string subjectName, string Language)
        {
            var subject = subjectsList.Where(sn => sn.Name.Equals(subjectName)).Select(s => s).ToArray()[0];
            if (db.UpdateSubjectLanguage(ref subject, Language))
            {
                //The record was updated
            }
            else
            {
                //Failed to update record
            }
        }
        public void RemoveSubject(string subject, int rowNumber)
        {
            SubjectO subjectObj = subjectsList.Where(sn => sn.Name.Equals(subject)).Select(sn => sn).ToArray()[0];
            if (subjectObj.Keywords.Count > 0)  //Have keywords
            {
                if (!Global.ShowDialogMessageYesNo("Are you sure you want delete this subject with all his keywords?")) return;  //Do not delete this subject 
            }
            try
            {
                foreach (KeywordO keyword in subjectObj.Keywords)  //Delete all keywords
                {
                    db.AddRemoveKeyword(Common.Action.REMOVE, subjectObj.ID, keyword.ID); //Remove keyword from DB
                    form.RemoveKeywordFromGrid(0);
                }
                db.AddRemoveSubject(Common.Action.REMOVE, subjectObj.ID);   //Remove subject from db
                form.RemoveSubjectFromGrid(rowNumber);    //Remove subject from UI
                CurrentSubject = null;
                subjectsList.Remove(subjectObj);
            }
            catch (Exception e)
            {
                Global.ShowDialogMessageOk(string.Format("Exception was throw while try to delete '{0}' subject: {1}", subject, e.Message));
            }
        }
        public void RemoveKeyword(string keyword, int rowNumber)
        {
            var key = CurrentSubject.Keywords.Where(k => k.Name.Equals(keyword)).Select(k => k).ToArray()[0]; //Get the key of the keyword
            try
            {
                db.AddRemoveKeyword(Common.Action.REMOVE, CurrentSubject.ID, key.ID); //Remove keyword from DB
                CurrentSubject.Keywords.Remove(key);
                form.RemoveKeywordFromGrid(rowNumber);
            }
            catch (Exception e)
            {
                Global.ShowDialogMessageOk(string.Format("Exception was throw while try to delete '{0}' keyword: {1}", keyword, e.Message));
            }
        }
        private void LoadSubjectsAndKeywords()
        {
            DataTable subjectsDT = db.GetActiveSubjects(true);
            foreach (DataRow dr in subjectsDT.Rows)    // Run on all subjects
            {
                SubjectO tmpSubject = new SubjectO(dr);
                DataTable keywordsDT = db.GetSubjectKeywordsDT((int)dr["ID"]);
                foreach (DataRow kdr in keywordsDT.Rows)
                {
                    tmpSubject.Keywords.Add(new KeywordO(kdr));
                }
                subjectsList.Add(tmpSubject);
            }
            form.LoadSubjects(subjectsList);
        }
        public void SetSelectedSubject(string subjectName)
        {
            var subject = subjectsList.Where(sn => sn.Name.Equals(subjectName)).Select(s => s).ToArray()[0];
            CurrentSubject = subject;
            form.LoadKeywords(subject.Keywords);
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
