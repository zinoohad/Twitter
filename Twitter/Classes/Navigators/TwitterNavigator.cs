using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class TwitterNavigator
    {
        public long next_cursor { get; set; }
        public string next_cursor_str { get; set; }
        public long previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }
    }
}
