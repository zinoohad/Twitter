using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitter.Classes;
using Twitter.Common;
using Twitter.Interface;

namespace Twitter.Forms
{
    public partial class TwitterResultDisplay : Form, Update
    {
        private Twitter twitter = new Twitter
        {
            OAuthConsumerKey = ConfigurationSettings.AppSettings["OAuthConsumerKey"],
            OAuthConsumerSecret = ConfigurationSettings.AppSettings["OAuthConsumerSecret"]
        };
        private List<Tweets> tweets = new List<Tweets>();


        public TwitterResultDisplay()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            t5LangCB.SelectedIndex = 0;
            t5ResultCB.SelectedIndex = 0;
            
        }
        #region Handlers
        private void GoB_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            long? userID = null;

            switch (b.Name)
            {
                case "t1GoB":
                    if (string.IsNullOrEmpty(t1UserNameTB.Text) && string.IsNullOrEmpty(t1UserIDTB.Text))
                    {
                        return;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(t1UserIDTB.Text))
                            userID = long.Parse(t1UserIDTB.Text);
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID","Create Date","Tweet","Language","Retweet Number","Hashtags"});
                    dgv.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    (new Thread(() => twitter.GetTweets(t1UserNameTB.Text, userID, (int)t1MaxTweetsUD.Value, (int)t5TweetsPerPageUD.Value, this))).Start();
                    break;
                case "t2GoB":
                    if (string.IsNullOrEmpty(t2UserNameTB.Text) && string.IsNullOrEmpty(t2UserIDTB.Text))
                    {
                        return;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(t2UserIDTB.Text))
                            userID = long.Parse(t2UserIDTB.Text);
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID"});
                    (new Thread(() => twitter.GetFriendsIDs(t2UserNameTB.Text, userID, this))).Start();
                    break;
                case "t3GoB":
                    break;
                case "t4GoB":
                    break;
                case "t5GoB":                    
                    if (string.IsNullOrEmpty(t5KeywordsTB.Text))
                    {
                        return;
                    }
                    string lang = t5LangCB.SelectedItem.Equals("Hebrew") ? "he" : "en";
                    string resultType = t5ResultCB.SelectedItem.Equals("Recent") ? "recent" : t5ResultCB.SelectedItem.Equals("Popular") ? "popular" : "mixed";                    
                    SetDGV(new string[] { "ID", "Create Date", "Tweet", "Language", "Retweet Number", "Hashtags" });    // Set headers
                    dgv.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;                    
                    (new Thread(() => twitter.SearchTweets(t5KeywordsTB.Text, (int)t5MaxTweetsUD.Value, (int)t5TweetsPerPageUD.Value, lang, resultType, t5IncEntCB.Checked,this))).Start();
                    break;
                case "t6GoB":
                    break;
            }
        }
        private void TB_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
                tb.BackColor = Color.Salmon;
            else tb.BackColor = SystemColors.Window;
        }
        #endregion
        #region Updates
        public void Update(object obj)
        {
            if(obj is List<Tweets>)
                dgv.Invoke(new MethodInvoker(() => { UpdateTweets((List<Tweets>)obj); }));
            if(obj is long[])
                dgv.Invoke(new MethodInvoker(() => { UpdateIDs((long[])obj); }));

        }
        public void UpdateIDs(long[] ids)
        {
            foreach (long id in ids)
                dgv.Rows.Add(id);
        }
        public void UpdateTweets(List<Tweets> tweets)
        {
            string hashtags;
            foreach (Tweets t in tweets)
            {
                hashtags = t.entities != null && t.entities.hashtags != null ? t.entities.hashtagsToString() : "";
                dgv.Rows.Add(t.id, t.created_at, t.text, t.user.lang, t.retweet_count, hashtags);
            }
        }
        #endregion
        #region DataGridView
        public void SetDGV(string[] headers)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnCount = headers.Length;
            for (int i = 0; i < headers.Length; i++)
                dgv.Columns[i].Name = headers[i];


        }
        #endregion

        

    }
}
