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

namespace TwitterCollector.Forms
{
    public partial class SubjectResult : Form
    {
        public SubjectResult()
        {
            InitializeComponent();
        }
        #region Handlers
        private void twitterTestUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.OpenTwitterRestAPI();
        }
        #endregion
    }
}
