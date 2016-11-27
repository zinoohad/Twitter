using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TwitterCollector
{
    public class Twitter
    {
        public string OAuthConsumerSecret { get; set; }        
        public string OAuthConsumerKey { get; set; }

        public async Task<IEnumerable<string>> GetTwitts(string userName,int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();   
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTwitts = (json as IEnumerable<dynamic>);

            if (enumerableTwitts == null)
            {
                return null;
            }
            return enumerableTwitts.Select(t => (string)(t["text"].ToString()));                        
        }

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
            return  item["access_token"];            
        }
        public async Task<Dictionary<string, object>> GetFriends(string userName, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/friends/ids.json?screen_name={0}", userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            List<Friends> ggg = JsonConvert.DeserializeObject<List<Friends>>(json);
           List< Dictionary<string, object>> enumerableTwitts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json); // (json as Dictionary<string, long[]>);
            //JsonSerializer serializer2 = new JsonSerializer();
            //serializer2.NullValueHandling = NullValueHandling.Ignore;
            //dynamic deserializedValue = JsonConvert.DeserializeObject(json);
            //var values = deserializedValue["ids"];
            //for (int i = 0; i < values.Count; i++)
            //{
            //    Console.WriteLine(values[i]["specific_field"]);
            //}

            //var ss = (long[]) enumerableTwitts["ids"];
            //List<Friends> friends = new List<Friends>();
            //foreach (long friend in ss)
            //{
            //    Friends f = new Friends();
            //    f.ID = friend;
            //}
            //if (enumerableTwitts == null)
            //{
            //    return null;
            //}
            return null; // enumerableTwitts.Select(t => (string)(t["text"].ToString()));
        }
    }
}