using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Twitter.Classes;
using Twitter.Classes.Navigators;

namespace Twitter
{
    public class Twitter
    {
        #region Params
        public string OAuthConsumerSecret { get; set; }        
        public string OAuthConsumerKey { get; set; }
        private const string serviceAddress = "https://api.twitter.com/1.1";    //Twitter API Address
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion
        #region Global Functions
        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.GetEncoding(1255), "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return item["access_token"];
        }
        private async Task<string> GetRequest(string requestUri, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);           
            string jsonStr = await responseUserTimeLine.Content.ReadAsStringAsync();
            return jsonStr;
        }
        #endregion
        #region GET Requests
        public async Task<List<Tweets>> GetTwitts(string userName, int maxTweets = 1000, int count = 200, string accessToken = null)
        {
            List<Tweets> t = new List<Tweets>();
            try
            {
                string requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName);
                string jsonStr = await GetRequest(requestUri);
                t = serializer.Deserialize<List<Tweets>>(jsonStr);
                t = t.OrderBy(x => x.id).ToList();
                long SinceID = t[0].id;
                while (t.Count < maxTweets)
                {
                    requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1&max_id={2}", count, userName, SinceID);
                    jsonStr = await GetRequest(requestUri);
                    t.AddRange(serializer.Deserialize<List<Tweets>>(jsonStr));
                    t = t.OrderBy(x => x.id).ToList();
                    SinceID = t[0].id;
                }
                return t;
            }
            catch { return t; }
        }
        public async Task<List<Tweets>> GetTwitts(long userID, int count = 200, string accessToken = null)
        {
            string requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&user_id={1}&trim_user=1&exclude_replies=1", count, userID);
            string jsonStr = await GetRequest(requestUri);
            List<Tweets> t = serializer.Deserialize<List<Tweets>>(jsonStr);
            return t;
        }   
        public async Task<FriendsNavigator> GetFriendsIDs(string userName, string accessToken = null)
        {
            string requestUri = string.Format(serviceAddress + "/friends/ids.json?screen_name={0}", userName);
            string jsonStr = await GetRequest(requestUri);
            FriendsNavigator f = serializer.Deserialize<FriendsNavigator>(jsonStr);
            return f;
        }
        public async Task<List<Users>> GetFriends(string userName, bool withTweets = false, int countPerPage = 10, string accessToken = null)
        {
            long cursor = -1;
            List<Users> users = new List<Users>();
            UsersNavigator f = new UsersNavigator();
            while (cursor != 0)
            {
                string requestUri = string.Format(serviceAddress + "/friends/list.json?screen_name={0}&skip_status={1}&cursor={2}&count={3}", userName, withTweets, cursor, countPerPage);
                string jsonStr = await GetRequest(requestUri);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    f = serializer.Deserialize<UsersNavigator>(jsonStr);
                    users.AddRange(f.users);
                    cursor = f.next_cursor;
                }
                catch (Exception e) { cursor = 0; }
            }
            return users;
        }

        #endregion
    }
}
#region Comments
/**
public async Task<Friends> GetFriends(string userName, string accessToken = null)
        {
            string requestUri = string.Format("https://api.twitter.com/1.1/friends/ids.json?screen_name={0}", userName);
            string jsonStr = await GetRequest(requestUri);
            Friends f = serializer.Deserialize<Friends>(jsonStr);
            return f;
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/friends/ids.json?screen_name={0}", userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            //var serializer = new JavaScriptSerializer();
            string jsonStr = await responseUserTimeLine.Content.ReadAsStringAsync();
            Friends f1 = serializer.Deserialize<Friends>(jsonStr);
            //JObject jsonDat = JObject.Parse(jsonStr);
            //Friends f = jsonDat.ToObject<Friends>();
            return f1;
        }
 **/
#endregion