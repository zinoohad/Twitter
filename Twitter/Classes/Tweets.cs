using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class Tweets
    {
        public Collection<Users> contributors { get; set; }
        public Coordinates coordinates { get; set; }
        public string created_at { get; set; }
        public Users current_user_retweet { get; set; }
        public Entities entities { get; set; }
        public int? favorite_count { get; set; }
        public bool? favorited { get; set; }
        public string filter_level { get; set; }
        public Coordinates geo { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string lang { get; set; }
        public Places place { get; set; }
        public bool? possibly_sensitive { get; set; }
        public long quoted_status_id { get; set; }
        public string quoted_status_id_str { get; set; }
        public Tweets quoted_status { get; set; }
        public Dictionary<string,bool?> scopes { get; set; }
        public int retweet_count { get; set; }
        public bool? retweeted { get; set; }
        public Tweets retweeted_status { get; set; }
        public string source { get; set; }
        public string text { get; set; }
        public bool? truncated { get; set; }
        public Users user { get; set; }
        public bool? withheld_copyright { get; set; }
        public string[] withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }

        public string[] GetData()
        {
            string[] row = new string[] {id.ToString(), created_at, text, lang, retweet_count.ToString(), entities.hashtagsToString()};
            return row;
        }




    }
    public class Coordinates
    {
        public Collection<float> coordinates { get; set; }
        public string type { get; set; }
    }

    

}
