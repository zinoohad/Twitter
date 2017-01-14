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
        public Collection<User> contributors { get; set; }
        public Coordinates coordinates { get; set; }
        [JsonProperty("created_at")]
        public string Date { get; set; }
        public User current_user_retweet { get; set; }
        public Entities entities { get; set; }
        [JsonProperty("favorite_count")]
        public int? FavoritesCount { get; set; }
        public bool? favorited { get; set; }
        public string filter_level { get; set; }
        public Coordinates geo { get; set; }
        [JsonProperty("id")]
        public long ID { get; set; }
        public string id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        public Place place { get; set; }
        public bool? possibly_sensitive { get; set; }
        public long quoted_status_id { get; set; }
        public string quoted_status_id_str { get; set; }
        public Tweet quoted_status { get; set; }
        public Dictionary<string,bool?> scopes { get; set; }
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }
        public bool? retweeted { get; set; }
        public Tweet retweeted_status { get; set; }
        public string source { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        public bool? truncated { get; set; }
        public User user { get; set; }
        public bool? withheld_copyright { get; set; }
        public string[] withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }


        #region Project Extra Fields
        public List<int> keywordID;
        public int? positiveWordCount;
        public int? negativeWordCount;
        #endregion

        public object[] GetData()
        {
            object[] row = new object[] { ID.ToString(), Date, Text, Language, RetweetCount.ToString(), entities.hashtagsToString() };
            return row;
        }

        public Tweet() { }
        public Tweet(DataRow dr, List<int> keywords = null)
        {
            ID = (long)dr["ID"];
            id_str = dr["ID"].ToString();
            Text = dr["Text"].ToString();
            Language = dr["Language"].ToString();
            Date = dr["Date"].ToString();
            RetweetCount = (int)dr["RetweetCount"];
            FavoritesCount = (int?)dr["FavoritesCount"];
            user = new User();
            user.ID = (long)dr["UserID"];
            user.id_str = dr["UserID"].ToString();
            if (keywords != null)
                keywordID = keywords;
            if (!dr.IsNull("RetweetID"))
            {
                retweeted_status = new Tweet();
                retweeted_status.ID = (long)dr["RetweetID"];
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
