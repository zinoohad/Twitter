using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web;
using Twitter.Classes;

namespace Twitter
{
    public class FriendsNavigator : TwitterNavigator
    {
        public long[] ids { get; set; }
    }
}
