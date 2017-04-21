using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopicSentimentAnalysis;
using TopicSentimentAnalysis.Classes;
using Twitter.Forms;
using TwitterCollector.Controllers;
using TwitterCollector.Forms;
using TwitterCollector.Timers;

namespace TwitterCollector.Common
{
    public static class Global
    {
        #region Forms

        public static CMain main;

        private static TwitterResultDisplay trd;

        private static CDictionaryManager dictionaryManager;

        private static CSubjectManager subjectManager;

        private static CSubjectResult subjectResult;

        private static CSettings settings;

        #endregion

        #region Global Params

       // public static DBHandler DB { get { return new DBHandler(); } }
        public static DBHandler DB { get { return new DBHandler(new DataBaseConnections.DBConnection(DataBaseConnections.DBTypes.SQLServer, "192.168.43.126", "1433", "Avi", "1234", "Twitter")); } }
        //new DataBaseConnections.DBConnection(DataBaseConnections.DBTypes.SQLServer, "192.168.1.12", "1433", "Avi", "1234", "Twitter")


        private static NewAgeWordsTimer ageWordsTimer;

        /// <summary>
        /// Value is true if the space is low.
        /// </summary>
        public static bool IsHardDriveSpaceLow
        {
            get
            {
                DBHandler db = DB;
                string driveName = db.GetValueByKey("DataBaseDriveNameStorage", "c").ToString();
                int lowLimitPercentages = int.Parse(db.GetValueByKey("DriveLowLimit", 5).ToString());
                float afs, ts;
                try
                {
                    DriveInfo di = new DriveInfo(driveName);
                    afs = (float)di.AvailableFreeSpace;
                    ts = (float)di.TotalSize;
                }
                catch (Exception e)
                {
                    throw new TwitterException(e);
                }

                int freeSpacePercentages = (int)(afs / ts * 100.0);
                if (freeSpacePercentages < lowLimitPercentages)
                    return true;
                return false;
            }
        }

        #endregion

        public static void OpenTwitterRestAPI()
        {
            if (trd == null)
                trd = new TwitterResultDisplay();
            trd.Show();
        }

        public static void ToolStripAction(string buttonName, BaseController sender)
        {
            Form form = new Form();
            switch (buttonName)
            {
                case "mainIcon":
                    if (sender == main) return;
                    form = main.GetUI();
                    form.Location = sender.GetUI().Location;
                    form.Show();
                    sender.GetUI().Hide();
                    break;
                case "subjectIcon":
                    if (subjectManager != null && sender == subjectManager) return;
                    if (subjectManager == null)
                    {
                        subjectManager = new CSubjectManager();    //Create Controller
                        subjectManager.GetUI().Location = sender.GetUI().Location;
                        sender.GetUI().Hide();
                        subjectManager.GetUI().ShowDialog();
                    }
                    else
                    {
                        form = subjectManager.GetUI();
                        form.Location = sender.GetUI().Location;
                        form.Show();
                        sender.GetUI().Hide();
                    }
                    break;
                case "statisticsIcon":
                    if (subjectResult != null && sender == subjectResult) return;
                    if (subjectResult == null)
                    {
                        subjectResult = new CSubjectResult();    //Create Controller
                        subjectResult.GetUI().Location = sender.GetUI().Location;
                        sender.GetUI().Hide();
                        subjectResult.GetUI().ShowDialog();
                    }
                    else
                    {
                        form = subjectResult.GetUI();
                        form.Location = sender.GetUI().Location;
                        form.Show();
                        sender.GetUI().Hide();
                    }
                    break;
                case "toolIcon":
                    if (trd == null || trd.IsDisposed)
                    {
                        trd = new TwitterResultDisplay();
                        (new Thread(() => trd.ShowDialog())).Start();
                        trd.FormClosing += delegate(object o, FormClosingEventArgs e) { trd = null; };
                    }
                    else trd.ExternalShow();//trd.Invoke(new MethodInvoker(() => { trd.Show(); })); //trd.Show();
                    
                    return;
                    //break;
                case "dictionaryIcon":
                    if (dictionaryManager != null && sender == dictionaryManager) return;
                    if (dictionaryManager == null)
                    {
                        dictionaryManager = new CDictionaryManager();    //Create Controller
                        dictionaryManager.GetUI().Location = sender.GetUI().Location;
                        sender.GetUI().Hide();
                        dictionaryManager.GetUI().ShowDialog();
                    }
                    else
                    {
                        form = dictionaryManager.GetUI();
                        form.Location = sender.GetUI().Location;
                        form.Show();
                        sender.GetUI().Hide();
                    }
                    break;
            }
           
            //form.Location = sender.GetUI().Location;
            //form.Show();
            //sender.GetUI().Hide();
        }

        public static void ExitApplication(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = CustomMessageBox.Show("Do you really want to exit? ", "Exit", CustomMessageBox.eDialogButtons.YesNo, TwitterCollector.Properties.Resources.Question, true);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
                
            }
        }

        public static bool ShowDialogMessageYesNo(string body, string title = "Attention", MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            DialogResult dialogResult = MessageBox.Show(body, title, MessageBoxButtons.YesNo, icon);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            else //if (dialogResult == DialogResult.No)
            {
                return false;
            }
        }

        public static void ShowDialogMessageOk(string body, string title = "Attention", MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(body, title, MessageBoxButtons.OK, icon);
        }

        public static T FillClassFromDataRow<T>(DataRow dr) where T : new()
        {
            var Class = new T();
            Type type = Class.GetType();
            FieldInfo[] fields = type.GetFields();
            MemberInfo[] mem = type.GetMembers();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!dr.Table.Columns.Contains(property.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[property.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    if (property.PropertyType == typeof(string))
                        property.SetValue(Class, dr[property.Name].ToString());
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                        property.SetValue(Class, int.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(bool))
                        property.SetValue(Class, bool.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(decimal))
                        property.SetValue(Class, decimal.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(float))
                        property.SetValue(Class, float.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(double))
                        property.SetValue(Class, double.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(long))
                        property.SetValue(Class, long.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Int32) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(uint)
                        || property.PropertyType == typeof(UInt16) || property.PropertyType == typeof(UInt32) || property.PropertyType == typeof(UInt64)
                        || property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(Single))
                        property.SetValue(Class, int.Parse(dr[property.Name].ToString()));
                    else property.SetValue(Class, dr[property.Name].ToString()); //Default set string
                }
                catch { Console.WriteLine(string.Format("No casting for {0} type.", property.PropertyType)); }
            }
            foreach (FieldInfo field in fields)
            {
                if (!dr.Table.Columns.Contains(field.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[field.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    if (field.FieldType == typeof(string))
                        field.SetValue(Class, dr[field.Name].ToString());
                    else if (field.FieldType == typeof(int) || field.FieldType == typeof(int?))
                        field.SetValue(Class, int.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(bool))
                        field.SetValue(Class, bool.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(decimal))
                        field.SetValue(Class, decimal.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(float))
                        field.SetValue(Class, float.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(double))
                        field.SetValue(Class, double.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(long))
                        field.SetValue(Class, long.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(Int16) || field.FieldType == typeof(Int32) || field.FieldType == typeof(Int64) || field.FieldType == typeof(uint)
                        || field.FieldType == typeof(UInt16) || field.FieldType == typeof(UInt32) || field.FieldType == typeof(UInt64)
                        || field.FieldType == typeof(sbyte) || field.FieldType == typeof(Single))
                        field.SetValue(Class, int.Parse(dr[field.Name].ToString()));
                    else field.SetValue(Class, dr[field.Name].ToString()); //Default set string
                }
                catch { Console.WriteLine(string.Format("No casting for {0} type.", field.FieldType)); }
            }
            return Class;
        }

        public static void Sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static bool In<T>(this T obj, params T[] args)
        {
            return args.Contains(obj);
        }

        public static bool IsEmoticon(string s)
        {
            if (s.Length < 2) return false;
            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                    return false;
            }
            return true;
        }

        public static string GetStringWithoutPunctuation(string oldString)
        {
            var newString = new StringBuilder();
            for (int i = 0; i < oldString.Length; i++)
            {
                char c = oldString[i];
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
                    || (i != 0 && i != oldString.Length - 1 && char.IsLetter(oldString[i - 1]) && char.IsLetter(oldString[i + 1]) && c.In('-', '\'')))
                    newString.Append(c);
            }
            return newString.ToString();
        }

        public static List<string> SplitSentenceToSubSentences(string sentence, int maxWordInSubSentence)
        {
            if (string.IsNullOrEmpty(sentence)) 
                return null;

            string[] spliteSentence = sentence.Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);
            int wordNumber = spliteSentence.Length;

            if (wordNumber < maxWordInSubSentence)
                maxWordInSubSentence = wordNumber;

            List<string> subSentenceList = new List<string>();
            int subSentenceLength, i;

            for (subSentenceLength = 1 ; subSentenceLength <= maxWordInSubSentence ; subSentenceLength++)
            {
                for (i = 0 ; i <= wordNumber - subSentenceLength ; i++)
                {
                    subSentenceList.Add(string.Join(" ", spliteSentence, i, subSentenceLength));
                }
            }
            return subSentenceList.Distinct().ToList();
        }

        #region Age Dictionary Learning

        /// <summary>
        /// This function get a sentence, split it to sub sentences and send it to external Api.
        /// The result from the api will save in the db and use to do user analysis.
        /// </summary>
        /// <param name="sentence"></param>
        public static void LearnNewWordsToAgeDictionary(string sentence, int maxWordInSubSentence = 3)
        {
            try
            {
                List<string> splitSentence = SplitSentenceToSubSentences(sentence, maxWordInSubSentence);
                List<WordAge> apiResult = WordSentimentAnalysis.CheckWordAge(splitSentence.ToArray());
                DBHandler db = DB;
                db.UpsertWordToAgeDictionaryAfterUsingAPI(apiResult);
            }
            catch (Exception e)
            {
                new TwitterException(e);
            }
        }

        /// <summary>
        /// This function get string values, it could be a sentence, sub sentence or word.
        /// The function works with timer and check all the words in intervals of the given value.
        /// </summary>
        /// <param name="values">Sentence, sub sentence or word.</param>
        public static void AddSentenceToBufferForChecking(params string[] values)
        {
            try
            {
                if (ageWordsTimer == null)
                {
                    DBHandler db = DB;
                    ageWordsTimer = new NewAgeWordsTimer();
                    ageWordsTimer.TimerInterval = int.Parse(
                        db.GetValueByKey("AgeWordsTimerIntervals", 60)
                        .ToString());
                }
                ageWordsTimer.Add(values);
            }
            catch (Exception e)
            {
                new TwitterException(e);
            }
        }

        #endregion

    }
}
