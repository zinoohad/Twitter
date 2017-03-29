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

namespace TwitterCollector.Forms
{
    public partial class DictionaryManager : Form, FormBase
    {
        #region Params
        private CDictionaryManager controller;
        #endregion
        public DictionaryManager()
        {
            InitializeComponent();
        }
        #region Handlers
        private void genderTrackB_Scroll(object sender, EventArgs e)
        {
            double value = genderTrackB.Value / 10000.0;
            genderUD.Value = decimal.Parse(value.ToString());
        }
        private void genderUD_Leave(object sender, EventArgs e)
        {
            genderTrackB.Value = (int)(genderUD.Value * 10000);
        }
        private void ageTB_Scroll(object sender, EventArgs e)
        {
            double value = ageTB.Value / 10000.0;
            ageUD.Value = decimal.Parse(value.ToString());
        }
        private void ageUD_Leave(object sender, EventArgs e)
        {
            ageTB.Value = (int)(ageUD.Value * 10000);
        }
        private void toolStripAction_Click(object sender, EventArgs e)
        {
            controller.ToolStripAction(((ToolStripButton)sender).Name);
        }
        private void onExit_Click(object sender, FormClosingEventArgs e)
        {
            Global.ExitApplication(sender, e);
        }
        #endregion

        public void SetController(BaseController controller)
        {
            this.controller = (CDictionaryManager) controller;
        }
    }
}
