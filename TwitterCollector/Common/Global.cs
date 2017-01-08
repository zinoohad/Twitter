using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    }
}
