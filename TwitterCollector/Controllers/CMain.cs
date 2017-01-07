using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Forms;
using TwitterCollector.Objects;

namespace TwitterCollector.Controllers
{
    public class CMain : BaseController
    {
        private Main form = new Main();
        public CMain()
        {
            form.SetController(this);
            LoadSubjectResult();
            Global.main = this;
            form.ShowDialog();
        }
        #region UI Functions
        #endregion
        #region Functions
        private void LoadSubjectResult()
        {
            form.LoadLastSubjectResults(new SubjectResultUI());
        }
        #endregion
        #region Implement Methods
        public override Form GetUI()
        {
            return form;
        }
        public override void ToolStripAction(string buttonName)
        {
            Global.ToolStripAction(buttonName,this);
        }
        #endregion
    }
}
