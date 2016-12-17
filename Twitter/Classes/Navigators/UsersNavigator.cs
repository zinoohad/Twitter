using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes.Navigators
{
    public class UsersNavigator : TwitterNavigator
    {
        public IList<User> users { get; set; }
    }
}
