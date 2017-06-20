using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Twitter.Common;

namespace TwitterCollector.Common
{
    public class TweetinviHelper
    {
        private DBHandler db = Global.DB;

        private TwitterKeys twitterKeys;

        public TweetinviHelper()
        {
            twitterKeys = db.GetTwitterKey();
            Auth.SetUserCredentials(twitterKeys.OAuthConsumerKey, twitterKeys.OAuthConsumerSecret, twitterKeys.AccessKey, twitterKeys.AccessSecret);
            //Auth.SetApplicationOnlyCredentials(twitterKeys.OAuthConsumerKey, twitterKeys.OAuthConsumerSecret);
            //var user = User.GetAuthenticatedUser();
            //var location = Geo.GetPlaceFromId("6017e966dbbf12c6");
            //var user = User.GetUserFromId(66);
        }
        
        public void UpdatePlaceState()
        {
            twitterKeys = db.GetTwitterKey();
            Auth.SetUserCredentials(twitterKeys.OAuthConsumerKey, twitterKeys.OAuthConsumerSecret, twitterKeys.AccessKey, twitterKeys.AccessSecret);
            //Auth.SetApplicationOnlyCredentials(twitterKeys.OAuthConsumerKey, twitterKeys.OAuthConsumerSecret);
            List<string> placeIDs = db.GetUncheckedPlaces();
            Tweetinvi.Models.IPlace place;
            foreach(string id in placeIDs)
            {
                try
                {
                    place = Geo.GetPlaceFromId(id);
                    if (place.ContainedWithin != null && place.ContainedWithin.Count > 0)
                    {
                        string postalCode = place.ContainedWithin[0].Name.Substring(place.ContainedWithin[0].Name.Length - 2, 2);
                        string stateName = place.ContainedWithin[0].Name.Substring(0, place.ContainedWithin[0].Name.Length - 3);
                        db.UpdatePlaceValues(id, stateName, postalCode);
                        //db.SetSingleValue("Places", "PlaceMainCountry", string.Format("PlaceID = {0}", id), place.ContainedWithin[0].Name);
                    }
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                    if (!e.Message.StartsWith("Error converting value"))
                    {
                        twitterKeys = db.GetTwitterKey();
                        Auth.SetUserCredentials(twitterKeys.OAuthConsumerKey, twitterKeys.OAuthConsumerSecret, twitterKeys.AccessKey, twitterKeys.AccessSecret);
                    }
                }
            }
        }

        public void UpdateAdminPlace()
        {
            DataTable dt = db.GetTable("Places", "PlaceType = 'admin' AND MainCountryPostalCode = 'is'");
            foreach (DataRow dr in dt.Rows)
            {
                string[] splitValue = dr["FullCountryName"].ToString().Split(new string[] {", "},StringSplitOptions.None);
                db.Update("UPDATE Places SET PlaceMainCountry = '{0}', MainCountryPostalCode = '{1}' WHERE ID = {2}", splitValue[0], splitValue[1], dr["ID"].ToString());
            }
        }
    }
}
