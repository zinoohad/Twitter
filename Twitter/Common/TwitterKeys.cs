using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Common
{
    public class TwitterKeys
    {   
        public readonly int ID;

        public string OAuthConsumerKey;

        public string OAuthConsumerSecret;

        public string AccessKey;

        public string AccessSecret;

        public TwitterKeys(int ID, string key1, string key2 = null, string key3 = null, string key4 = null)
        {
            this.ID = ID;
            OAuthConsumerKey = key1;
            OAuthConsumerSecret = key2;
            AccessKey = key3;
            AccessSecret = key4;
        }
    }

}
