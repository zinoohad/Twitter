using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class Entities
    {
        public Hashtag[] hashtags { get; set; }
        public Media[] media { get; set; }
        public URL[] urls { get; set; }
        public IList<object> symbols { get; set; }
        public UserMention[] user_mentions { get; set; }

        public string hashtagsToString()
        {
            string s = "";
            foreach (Hashtag h in hashtags)
                s += h.text + " , ";
            return s;
        }
    }

    public class Hashtag
    {
        public int[] indices { get; set; }
        public string text { get; set; }
    }

    public class Media
    {
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public int[] indices { get; set; }
        public string media_url { get; set; }
        public string media_url_https { get; set; }
        public Sizes sizes { get; set; }
        public long source_status_id { get; set; }
        public string source_status_id_str { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Size
    {
        public int h { get; set; }
        public string resize { get; set; }
        public int w { get; set; }
    }
    public class Sizes
    {
        public Size thumb { get; set; }
        public Size large { get; set; }
        public Size medium { get; set; }
        public Size small { get; set; }
    }
    public class URL
    {
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public IList<int> indices { get; set; }
        public string url { get; set; }
    }
    public class UserMention
    {
        public long id { get; set; }
        public string id_str { get; set; }
        public int[] indices { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
    }

}

