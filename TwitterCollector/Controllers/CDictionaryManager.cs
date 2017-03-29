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
    public class CDictionaryManager : BaseController
    {
        #region Params
        DictionaryManager form = new DictionaryManager();
        #endregion
        public CDictionaryManager()
        {
            form.SetController(this);
            //form.ShowDialog();
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
