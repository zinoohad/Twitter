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
using TwitterCollector.Objects;

namespace TwitterCollector.Forms
{
    public partial class Main : Form, FormBase
    {
        #region Params
        private CMain controller;
        #endregion
        public Main()
        {
            InitializeComponent();
        }
        #region Handlers
        private void toolStripAction_Click(object sender, EventArgs e)
        {
            controller.ToolStripAction(((ToolStripButton)sender).Name);
        }
        private void onExit_Click(object sender, FormClosingEventArgs e)
        {
            Global.ExitApplication(sender, e);
        }
        #endregion
        #region Interface Implement
        public void SetController(BaseController controller)
        {
            this.controller = (CMain)controller;
        }
        #endregion
        #region Functions
        public void LoadLastSubjectResults(SubjectResultUI values)
        {
        }
        #endregion
    }
}
