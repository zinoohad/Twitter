using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Forms;

namespace TwitterCollector.Controllers
{
    class CSettings : BaseController
    {
        private Settings form = new Settings();

        public CSettings()
        {
            form.SetController(this);
        }

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
