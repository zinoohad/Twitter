using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class User
    {
        public bool? contributors_enabled { get; set; }
        [JsonProperty("created_at")]
        public string CreateDate { get; set; }
        public bool? default_profile { get; set; }
        public bool? default_profile_image { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public Entities entities { get; set; }
        public int favourites_count { get; set; }
        public bool? follow_request_sent { get; set; }
        public bool? following { get; set; }
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }
        public bool? geo_enabled { get; set; }
        [JsonProperty("id")]
        public long ID { get; set; }
        public string id_str { get; set; }
        public bool? is_translator { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        public int listed_count { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public bool? notifications { get; set; }
        public string profile_background_color { get; set; }
        [JsonProperty("profile_background_image_url")]
        public string BackgroundImage { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool? profile_background_tile { get; set; }
        [JsonProperty("profile_banner_url")]
        public string BannerImage { get; set; }
        [JsonProperty("profile_image_url")]
        public string ProfileImage { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool? profile_use_background_image { get; set; }
        public bool? Protected { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        public bool? show_all_inline_media { get; set; }
        public Tweet status { get; set; }
        public int statuses_count { get; set; }
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
        public string url { get; set; }
        public int? utc_offset { get; set; }
        public bool? verified { get; set; }
        public IList<string> withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }

        public IList<Error> errors { get; set; }

        #region External Params

        public int AccountAge { get; set; }

        public int UserPropertiesID { get; set; }

        #endregion


        public object[] GetData()
        {
            object[] row = new object[] { ID, CreateDate, Name, Name, FollowersCount, FriendsCount, Language, Location };
            return row;
        }
        public User() { }
    }
}
