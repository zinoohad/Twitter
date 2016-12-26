using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Twitter.Classes
{
    public class Tweet
    {
        //[XmlElement(ElementName = "TaxRate")]
        
        public Collection<User> contributors { get; set; }
        public Coordinates coordinates { get; set; }
        //[JsonProperty(PropertyName = "created_at")]
        //[XmlElement(ElementName = "created_at")]
        public string created_at { get; set; }
        public User current_user_retweet { get; set; }
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
        public Place place { get; set; }
        public bool? possibly_sensitive { get; set; }
        public long quoted_status_id { get; set; }
        public string quoted_status_id_str { get; set; }
        public Tweet quoted_status { get; set; }
        public Dictionary<string,bool?> scopes { get; set; }
        public int retweet_count { get; set; }
        public bool? retweeted { get; set; }
        public Tweet retweeted_status { get; set; }
        public string source { get; set; }
        public string text { get; set; }
        public bool? truncated { get; set; }
        public User user { get; set; }
        public bool? withheld_copyright { get; set; }
        public string[] withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }


        #region Project Extra Fields
        public int? keywordID;
        public int? positiveWordCount;
        public int? negativeWordCount;
        #endregion

        public object[] GetData()
        {
            object[] row = new object[] { id.ToString(), created_at, text, lang, retweet_count.ToString(), entities.hashtagsToString() };
            return row;
        }

        public Tweet() { }
        public Tweet(DataRow dr)
        {

            id = (long)dr["ID"];
            id_str = dr["ID"].ToString();
            text = dr["Text"].ToString();
            lang = dr["Language"].ToString();
            created_at = dr["Date"].ToString();
            retweet_count = (int)dr["RetweetCount"];
            favorite_count = (int?)dr["FavoritesCount"];
            user = new User();
            user.id = (long)dr["UserID"];
            user.id_str = dr["UserID"].ToString();
            keywordID = (int?)dr["SubjectKeyword"];
            if (!dr.IsNull("RetweetID"))
            {
                retweeted_status = new Tweet();
                retweeted_status.id = (long)dr["RetweetID"];
                retweeted_status.id_str = dr["RetweetID"].ToString();
            }
        }



    }
    public class Coordinates
    {
        public Collection<float> coordinates { get; set; }
        public string type { get; set; }
    }

    

}
