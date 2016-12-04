﻿using Newtonsoft.Json;
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
using Twitter.Interface;

namespace Twitter
{
    public class Twitter
    {
        #region Params
        public string OAuthConsumerSecret { get; set; }        
        public string OAuthConsumerKey { get; set; }
        private const string serviceAddress = "https://api.twitter.com/1.1";    //Twitter API Address
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private Encoding requestEncoding = Encoding.GetEncoding(1255);  //Hebrew
        #endregion
        #region Global Functions
        /// <summary>
        /// Account Authorization.
        /// </summary>
        /// <returns>Access Token.</returns>
        public string GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", requestEncoding, "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            string json = response.Content.ReadAsStringAsync().Result;
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return item["access_token"];
        }
        /// <summary>
        /// Send an GET request to Twitter REST API
        /// </summary>
        /// <param name="requestUri">The request URI</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>JSON object.</returns>
        private string GetRequest(string requestUri, string accessToken = null)
        {
            if (accessToken == null)
                accessToken = GetAccessToken();

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;           
            string jsonStr = responseUserTimeLine.Content.ReadAsStringAsync().Result;
            return jsonStr;
        }
        #endregion
        #region GET Requests
         ///<summary>
         ///Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters.
         ///User timelines belonging to protected users may only be requested when the authenticated user either “owns” the timeline or is an approved follower of the owner.
         ///The timeline returned is the equivalent of the one seen as a user’s profile on twitter.com.
         ///This method can only return up to 3,200 of a user’s most recent Tweets. Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.
         ///</summary>
         ///<param name="userName">The screen name of the user for whom to return results for.</param>
         ///<param name="userID">The ID of the user for whom to return results for.</param>
         ///<param name="maxTweets">Returns results with an ID less than (that is, older than) or equal to the specified ID.</param>
         ///<param name="countPerPage">Specifies the number of Tweets to try and retrieve, up to a maximum of 200 per distinct request. The value of count is best thought of as a limit to the number of Tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</param>
         ///<param name="accessToken">Optional access token.</param>
         ///<returns>List of tweets.</returns>
        public List<Tweets> GetTweets(string userName, long? userID = null, int maxTweets = 3200, int countPerPage = 200, Update updater = null, string accessToken = null)
        {
            List<Tweets> t = new List<Tweets>();
            string requestUri, requestUriWithCursor, jsonStr;
            if (userID == null)
                requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", countPerPage, userName);
            else
                requestUri = string.Format(serviceAddress + "/statuses/user_timeline.json?count={0}&user_id={1}&trim_user=1&exclude_replies=1", countPerPage, userID);
            try
            {
                jsonStr = GetRequest(requestUri);
                t = serializer.Deserialize<List<Tweets>>(jsonStr);
                t = t.OrderBy(x => x.id).ToList();
                if (updater != null) // Add new tweets to UI
                    updater.Update(t);
                long SinceID = t[0].id;
                while (t.Count < maxTweets)
                {
                    requestUriWithCursor = string.Format(requestUri + "&max_id={0}", SinceID);
                    jsonStr = GetRequest(requestUriWithCursor);
                    List<Tweets> tmp = serializer.Deserialize<List<Tweets>>(jsonStr);
                    t.AddRange(tmp);
                    tmp = tmp.OrderBy(x => x.id).ToList();
                    if (updater != null) // Add new tweets to UI
                        updater.Update(tmp);
                    //t = t.OrderBy(x => x.id).ToList();
                    SinceID = tmp[0].id;
                    if (tmp.Count <= 5)
                    {
                        if (updater != null) updater.EndRequest();
                        return t;
                    }
                }
                if (updater != null) updater.EndRequest();
                return t;
            }
            catch { if (updater != null)updater.EndRequest(); return t; }
        }     
        /// <summary>
        /// Returns a cursored collection of user IDs for every user the specified user is friends (otherwise known as their “follows”).
        /// </summary>
        /// <param name="userName">The screen name of the user for whom to return results for.</param>
        /// <param name="userID">The ID of the user for whom to return results for.</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>List of id's.</returns>
        public List<long> GetFriendsIDs(string userName, long? userID = null, Update updater = null, string accessToken = null)
        {
            List<long> friendsIDs = new List<long>();
            long cursor = -1;
            FriendsNavigator f = new FriendsNavigator();
            string requestUri, requestUriWithCursor, jsonStr;
            if(userID == null)
                requestUri = string.Format(serviceAddress + "/friends/ids.json?screen_name={0}", userName);
            else
                requestUri = string.Format(serviceAddress + "/friends/ids.json?user_id={0}", userID);
            while (cursor != 0)
            {
                requestUriWithCursor = string.Format(requestUri + "&cursor={0}", cursor);
                jsonStr = GetRequest(requestUriWithCursor);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    f = serializer.Deserialize<FriendsNavigator>(jsonStr);
                    if (updater != null) // Add new IDs to UI
                        updater.Update(f.ids);
                    friendsIDs.AddRange(f.ids);
                    cursor = f.next_cursor;
                }
                catch { cursor = 0; }
            }
            if (updater != null) updater.EndRequest();
            return friendsIDs;
        }
        /// <summary>
        /// Returns a cursored collection of user objects for every user the specified user is following (otherwise known as their “friends”).
        /// </summary>
        /// <param name="userName">The screen name of the user for whom to return results.</param>
        /// <param name="userID">The ID of the user for whom to return results.</param>
        /// <param name="withTweets">When set to either true, t or 1 statuses will not be included in the returned user objects.</param>
        /// <param name="countPerPage">The number of users to return per page, up to a maximum of 200. Defaults to 20.</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>List of users.</returns>
        public List<Users> GetFriends(string userName, long? userID = null, bool withoutTweets = true, int countPerPage = 200, Update updater = null, string accessToken = null)
        {
            long cursor = -1;
            List<Users> users = new List<Users>();
            UsersNavigator u = new UsersNavigator();
            string requestUri, requestUriWithCursor, jsonStr;
            if (userID == null)
                requestUri = string.Format(serviceAddress + "/friends/list.json?screen_name={0}&skip_status={1}&count={2}", userName, withoutTweets, countPerPage);
            else
                requestUri = string.Format(serviceAddress + "/friends/list.json?user_id={0}&skip_status={1}&count={2}", userID, withoutTweets, countPerPage);
            while (cursor != 0)
            {
                requestUriWithCursor = string.Format(requestUri + "&cursor={0}", cursor);
                jsonStr = GetRequest(requestUriWithCursor);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    u = serializer.Deserialize<UsersNavigator>(jsonStr);
                    if (cursor == u.next_cursor)
                        break;
                    else
                        cursor = u.next_cursor;
                    if (updater != null) // Add new IDs to UI
                        updater.Update(u.users);
                    users.AddRange(u.users);
                }
                catch { cursor = 0; }
            }
            if (updater != null) updater.EndRequest();
            return users;
        }
        public List<long> GetFollowersIDs(string userName, long? userID = null, Update updater = null, string accessToken = null)
        {
            List<long> friendsIDs = new List<long>();
            long cursor = -1;
            FriendsNavigator f = new FriendsNavigator();
            string requestUri, requestUriWithCursor, jsonStr;
            if (userID == null)
                requestUri = string.Format(serviceAddress + "/followers/ids.json?screen_name={0}", userName);
            else
                requestUri = string.Format(serviceAddress + "/followers/ids.json?user_id={0}", userID);
            while (cursor != 0)
            {
                requestUriWithCursor = string.Format(requestUri + "&cursor={0}", cursor);
                jsonStr = GetRequest(requestUriWithCursor);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    f = serializer.Deserialize<FriendsNavigator>(jsonStr);
                    if (updater != null) // Add new IDs to UI
                        updater.Update(f.ids);
                    friendsIDs.AddRange(f.ids);
                    cursor = f.next_cursor;
                }
                catch { cursor = 0; }
            }
            if (updater != null) updater.EndRequest();
            return friendsIDs;
        }
        /// <summary>
        /// Returns a cursored collection of user objects for users following the specified user.
        /// </summary>
        /// <param name="userName">The screen name of the user for whom to return results.</param>
        /// <param name="userID">The ID of the user for whom to return results.</param>
        /// <param name="withTweets">When set to either true, t or 1 statuses will not be included in the returned user objects.</param>
        /// <param name="countPerPage">The number of users to return per page, up to a maximum of 200. Defaults to 20.</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>List of users.</returns>
        public List<Users> GetFollowers(string userName, long? userID = null, bool withoutTweets = true, int countPerPage = 200, Update updater = null, string accessToken = null)
        {
            long cursor = -1;
            List<Users> users = new List<Users>();
            UsersNavigator u = new UsersNavigator();
            string requestUri, requestUriWithCursor, jsonStr;
            if (userID == null)
                requestUri = string.Format(serviceAddress + "/followers/list.json?screen_name={0}&skip_status={1}&count={2}", userName, withoutTweets, countPerPage);
            else
                requestUri = string.Format(serviceAddress + "/followers/list.json?user_id={0}&skip_status={1}&count={2}", userID, withoutTweets, countPerPage);
            while (cursor != 0)
            {
                requestUriWithCursor = string.Format(requestUri + "&cursor={0}", cursor);
                jsonStr = GetRequest(requestUri);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    u = null;
                    u = serializer.Deserialize<UsersNavigator>(jsonStr);
                    if (cursor == u.next_cursor)
                        break;
                    else
                        cursor = u.next_cursor;
                    if (updater != null) // Add new IDs to UI
                        updater.Update(u.users);
                    users.AddRange(u.users);
                }
                catch { cursor = 0; }
            }
            if (updater != null) updater.EndRequest();
            return users;
        }
        /// <summary>
        /// Get tweets that contains given keyword
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
        public List<Tweets> SearchTweets(string keywords, int maxTweets = 2000, int countPerPage = 100, string language = "he", string resultType = "recent", bool includeEntities = false, Update updater = null, string accessToken = null)
        {
            List<Tweets> t = new List<Tweets>();
            SearchTweetsNavigator stn = new SearchTweetsNavigator();
            string requestUri, requestUriWithCursor, jsonStr;
            try
            {
                keywords = keywords.Replace(' ', '+').Replace("#", "%23").Replace("“", "%22");
                requestUri = string.Format(serviceAddress + "/search/tweets.json?q={0}&count={1}&lang={2}&result_type={3}&include_entities={4}", keywords, countPerPage, language, resultType, includeEntities);
                jsonStr = GetRequest(requestUri);
                stn = serializer.Deserialize<SearchTweetsNavigator>(jsonStr);
                t.AddRange(stn.statuses);
                t = t.OrderBy(x => x.id).ToList();
                if (updater != null) // Add new tweets to UI
                    updater.Update(t);
                long SinceID = t[0].id;
                while (t.Count < maxTweets)
                {
                    requestUriWithCursor = string.Format(requestUri + "&max_id={0}", SinceID);
                    jsonStr = GetRequest(requestUriWithCursor);
                    stn = serializer.Deserialize<SearchTweetsNavigator>(jsonStr);
                    List<Tweets> tmp = stn.statuses.OrderBy(x => x.id).ToList();
                    t.AddRange(tmp);
                    if (updater != null)    // Add new tweets to UI
                        updater.Update(tmp);
                    //t = t.OrderBy(x => x.id).ToList();
                    SinceID = tmp[0].id;
                    if (tmp.Count <= 5)
                    {
                        if (updater != null) updater.EndRequest();
                        return t;
                    }
                }
                if (updater != null) updater.EndRequest();
                return t;
            }
            catch { if (updater != null)updater.EndRequest(); return t; }
        }
        /// <summary>
        /// Returns a collection of up to 100 user IDs belonging to users who have retweeted the Tweet specified by the id parameter.
        /// </summary>
        /// <param name="tweetID">The numerical ID of the desired status.</param>
        /// <param name="accessToken">Optional access token.</param>
        /// <returns>List of id's.</returns>
        public List<long> GetRetweetIDs(string tweetID, Update updater = null, string accessToken = null)
        {
            List<long> friendsIDs = new List<long>();
            long cursor = -1;
            RetweetNavigator r = new RetweetNavigator();
            string requestUriWithCursor, jsonStr;
            string requestUri = string.Format(serviceAddress + "/statuses/retweeters/ids.json?id={0}", tweetID);

            while (cursor != 0)
            {
                requestUriWithCursor = string.Format(requestUri + "&cursor={0}", cursor);
                jsonStr = GetRequest(requestUriWithCursor);
                //JObject jsonDat = JObject.Parse(jsonStr);                
                try
                {
                    r = serializer.Deserialize<RetweetNavigator>(jsonStr);
                    if (updater != null) // Add new IDs to UI
                        updater.Update(r.ids);
                    friendsIDs.AddRange(r.ids);
                    cursor = r.next_cursor;
                }
                catch { cursor = 0; }
            }
            if (updater != null) updater.EndRequest();
            return friendsIDs;
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