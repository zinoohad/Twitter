using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;

namespace TwitterCollector.Controllers
{
    public abstract class BaseController 
    {
        protected DBHandler db = new DBHandler();
        public abstract Form GetUI();
        public abstract void ToolStripAction(string buttonName);
    }

}
