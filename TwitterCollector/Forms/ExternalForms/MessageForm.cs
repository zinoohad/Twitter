using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwitterCollector.Forms.ExternalForms
{
    internal partial class MessageForm : Form
    {
        internal MessageForm()
        {
            InitializeComponent();
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public void changeExitStayButtons()
        {
            btnNo.Text = "Exit";
            btnNo.Font = new Font(btnNo.Font, FontStyle.Bold);
            btnNo.BackColor = Color.CornflowerBlue;
            btnNo.ForeColor = Color.WhiteSmoke;
            btnYes.Text = "Stay";
            this.Width = 420;
            

        }
    }
}
