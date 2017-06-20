using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Timers;

namespace TwitterCollector.Controllers
{
    public abstract class BaseController 
    {
        protected DBHandler db = Global.DB;

        protected UpdateUiTimer _updateUiTimer = new UpdateUiTimer();

        public abstract Form GetUI();

        public abstract Form GetUI(UiState state);

        public abstract void ToolStripAction(string buttonName);


    }

}
