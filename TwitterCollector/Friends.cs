using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web;

namespace TwitterCollector
{
    public class Friends
    {
        public Object[] ids { get; set; }
        public object next_cursor { get; set; }
        public object next_cursor_str { get; set; }
        public object previous_cursor { get; set; }
        public object previous_cursor_str { get; set; }
    }
}
