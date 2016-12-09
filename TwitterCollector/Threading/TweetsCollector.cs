using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;

namespace TwitterCollector.Threading
{
    public class TweetsCollector : BaseThread
    {
        private DBHandler db = new DBHandler();
        public override void RunThread()
        {
            // Do some stuff
        }
    }
}
