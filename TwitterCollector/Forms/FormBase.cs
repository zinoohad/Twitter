using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Controllers;

namespace TwitterCollector.Forms
{
    public interface FormBase
    {
        void SetController(BaseController controller);
    }
}
