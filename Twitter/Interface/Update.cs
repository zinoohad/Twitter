using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Classes;

namespace Twitter.Interface
{
    public interface Update
    {
        void UpdateTweets(List<Tweets> tweets);
        

    }
}
