using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Forms;

namespace Twitter.Controllers
{
    public class CTwitterResultDisplay
    {
        public CTwitterResultDisplay()
        {
            (new TwitterResultDisplay()).Show();
        }
    }
}
