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
        public async Task<List<Tweets>> GetTwitts(long userID, int maxTweets = 1000, int count = 200, string accessToken = null)
        {
            List<Tweets> t = new List<Tweets>();
            try
            {
                string requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&user_id={1}&trim_user=1&exclude_replies=1", count, userID);
                string jsonStr = await GetRequest(requestUri);
                t = serializer.Deserialize<List<Tweets>>(jsonStr);
                t = t.OrderBy(x => x.id).ToList();
                long SinceID = t[0].id;
                while (t.Count < maxTweets)
                {
                    requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&user_id={1}&trim_user=1&exclude_replies=1&max_id={2}", count, userID, SinceID);
                    jsonStr = await GetRequest(requestUri);
                    t.AddRange(serializer.Deserialize<List<Tweets>>(jsonStr));
                    t = t.OrderBy(x => x.id).ToList();
                    SinceID = t[0].id;
                }
                return t;
            }
            catch { return t; }
        }
        public async Task<List<long>> GetFriendsIDs(string userName, string accessToken = null)
        {
            List<long> friendsIDs = new List<long>();
            long cursor = -1;
            FriendsNavigator f = new FriendsNavigator();
            while (cursor != 0)
            {
                string requestUri = string.Format(serviceAddress + "/friends/ids.json?screen_name={0}&cursor={1}", userName, cursor);
                string jsonStr = await GetRequest(requestUri);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    f = serializer.Deserialize<FriendsNavigator>(jsonStr);
                    friendsIDs.AddRange(f.ids);
                    cursor = f.next_cursor;
                }
                catch (Exception e) { cursor = 0; }
            }
            return friendsIDs;
        }
        public async Task<List<long>> GetFriendsIDs(long userID, string accessToken = null)
        {
            List<long> friendsIDs = new List<long>();
            long cursor = -1;
            FriendsNavigator f = new FriendsNavigator();
            while (cursor != 0)
            {
                string requestUri = string.Format(serviceAddress + "/friends/ids.json?user_id={0}&cursor={1}", userID, cursor);
                string jsonStr = await GetRequest(requestUri);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    f = serializer.Deserialize<FriendsNavigator>(jsonStr);
                    friendsIDs.AddRange(f.ids);
                    cursor = f.next_cursor;
                }
                catch (Exception e) { cursor = 0; }
            }
            return friendsIDs;
        }
        public async Task<List<Users>> GetFriends(string userName, bool withTweets = false, int countPerPage = 10, string accessToken = null)
        {
            long cursor = -1;
            List<Users> users = new List<Users>();
            UsersNavigator u = new UsersNavigator();
            while (cursor != 0)
            {
                string requestUri = string.Format(serviceAddress + "/friends/list.json?screen_name={0}&skip_status={1}&cursor={2}&count={3}", userName, withTweets, cursor, countPerPage);
                string jsonStr = await GetRequest(requestUri);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    u = serializer.Deserialize<UsersNavigator>(jsonStr);
                    users.AddRange(u.users);
                    cursor = u.next_cursor;
                }
                catch (Exception e) { cursor = 0; }
            }
            return users;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywords">A UTF-8, URL-encoded search query of 500 characters maximum, including operators. Queries may additionally be limited by complexity.</param>
        /// <param name="countPerPage">The number of tweets to return per page, up to a maximum of 100. Defaults to 15. This was formerly the “rpp” parameter in the old Search API.</param>
        /// <param name="language">Restricts tweets to the given language, given by an ISO 639-1 code. Language detection is best-effort. [https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes]</param>
        /// <param name="resultType">mixed : Include both popular and real time results in the response.
        ///                         recent : return only the most recent results in the response
        ///                         popular : return only the most popular results in the response.</param>
        /// <param name="includeEntities">The entities node will not be included when set to false.	</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>List of tweets.</returns>
        public async Task<List<Tweets>> SearchTweets(string keywords, int maxTweets = 2000, int countPerPage = 100, string language = "he", string resultType = "recent", bool includeEntities = false, string accessToken = null)
        {
            List<Tweets> t = new List<Tweets>();
            SearchTweetsNavigator stn = new SearchTweetsNavigator();
            try
            {
                keywords = keywords.Replace(' ', '+').Replace("#", "%23").Replace("“", "%22");
                string requestUri = string.Format(serviceAddress + "/search/tweets.json?q={0}&count={1}&lang={2}&result_type={3}&include_entities={4}", keywords, countPerPage, language, resultType, includeEntities);
                string jsonStr = await GetRequest(requestUri);
                stn = serializer.Deserialize<SearchTweetsNavigator>(jsonStr);
                t.AddRange(stn.statuses);
                t = t.OrderBy(x => x.id).ToList();
                long SinceID = t[0].id;
                while (t.Count < maxTweets)
                {
                    requestUri = string.Format(serviceAddress + "/search/tweets.json?q={0}&count={1}&lang={2}&result_type={3}&include_entities={4}&max_id={5}", keywords, countPerPage, language, resultType, includeEntities, SinceID);
                    jsonStr = await GetRequest(requestUri);
                    stn = serializer.Deserialize<SearchTweetsNavigator>(jsonStr);
                    t.AddRange(stn.statuses);
                    t = t.OrderBy(x => x.id).ToList();
                    SinceID = t[0].id;
                }
                return t;
            }
            catch { return t; }
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