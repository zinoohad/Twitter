using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitter.Forms;
using TwitterCollector.Controllers;
using TwitterCollector.Forms;

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
    }
}
