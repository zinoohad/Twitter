using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;

namespace TwitterCollector.Controllers
{
    public class BaseController
    {
        protected DBHandler db = new DBHandler();
    }
}
