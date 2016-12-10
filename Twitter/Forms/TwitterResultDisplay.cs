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
        private TwitterAPI twitter = new TwitterAPI();
        private List<Tweets> tweets = new List<Tweets>();
        private int recordNumber = 0;

        public TwitterResultDisplay()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            t5LangCB.SelectedIndex = 0;
            t5ResultCB.SelectedIndex = 0;
            hideGif();
            
        }
        #region Handlers
        private void GoB_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            long? userID = null;
            AddRecord(0);
            showGif();
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
                    SetDGV(new string[] { "ID", "Create Date", "Tweet", "Language", "Retweet Number", "Like Number", "Hashtags" });
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
                    if (string.IsNullOrEmpty(t3UserNameTB.Text) && string.IsNullOrEmpty(t3UserIDTB.Text))
                    {
                        return;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(t3UserIDTB.Text))
                            userID = long.Parse(t3UserIDTB.Text);
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID", "Create Date", "Name", "Screen Name", "Followers Count", "Friends Count","Language","Location"});
                    (new Thread(() => twitter.GetFriends(t3UserNameTB.Text, userID, t3WithoutTweetCB.Checked,(int)t2TweetsPerPageUD.Value, this))).Start();
                    break;
                case "t4GoB":
                    if (string.IsNullOrEmpty(t4UserNameTB.Text) && string.IsNullOrEmpty(t4UserIDTB.Text))
                    {
                        return;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(t4UserIDTB.Text))
                            userID = long.Parse(t4UserIDTB.Text);
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID", "Create Date", "Name", "Screen Name", "Followers Count", "Friends Count","Language","Location"});
                    (new Thread(() => twitter.GetFollowers(t4UserNameTB.Text, userID, t4WithoutTweetCB.Checked, (int)t4TweetsPerPageUD.Value, this))).Start();
                    break;
                case "t5GoB":                    
                    if (string.IsNullOrEmpty(t5KeywordsTB.Text))
                    {
                        return;
                    }
                    string lang = t5LangCB.SelectedItem.Equals("Hebrew") ? "he" : "en";
                    string resultType = t5ResultCB.SelectedItem.Equals("Recent") ? "recent" : t5ResultCB.SelectedItem.Equals("Popular") ? "popular" : "mixed";                    
                    SetDGV(new string[] { "ID", "Create Date", "Tweet", "Language", "Retweet Number","Like Number", "Hashtags" });    // Set headers
                    dgv.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;                    
                    (new Thread(() => twitter.SearchTweets(t5KeywordsTB.Text, (int)t5MaxTweetsUD.Value, (int)t5TweetsPerPageUD.Value, lang, resultType, t5IncEntCB.Checked,this))).Start();
                    break;
                case "t6GoB":
                    try
                    {
                        if (!string.IsNullOrEmpty(t6TweetIDCB.Text))
                            userID = long.Parse(t6TweetIDCB.Text);
                        else return;
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID"});
                    (new Thread(() => twitter.GetRetweetIDs(t6TweetIDCB.Text, this))).Start();
                    break;
                case "t7GoB":
                    if (string.IsNullOrEmpty(t7UserNameTB.Text) && string.IsNullOrEmpty(t7UserIDTB.Text))
                    {
                        return;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(t7UserIDTB.Text))
                            userID = long.Parse(t7UserIDTB.Text);
                    }
                    catch { return; }
                    SetDGV(new string[]{"ID"});
                    (new Thread(() => twitter.GetFollowersIDs(t7UserNameTB.Text, userID, this))).Start();
                    break;
                default: hideGif();
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
            else if(obj is long[])
                dgv.Invoke(new MethodInvoker(() => { UpdateIDs((long[])obj); }));
            else if(obj is List<Users>)
                dgv.Invoke(new MethodInvoker(() => { UpdateUsers((List<Users>)obj); }));
        }
        public void UpdateIDs(long[] ids)
        {
            foreach (long id in ids)
            {
                dgv.Rows.Add(id);
                AddRecord();
            }
        }
        public void UpdateTweets(List<Tweets> tweets)
        {
            string hashtags;
            foreach (Tweets t in tweets)
            {
                hashtags = t.entities != null && t.entities.hashtags != null ? t.entities.hashtagsToString() : "";
                dgv.Rows.Add(t.id, t.created_at, t.text, t.user.lang, t.retweet_count,t.favorite_count, hashtags);
                AddRecord();
            }
        }
        public void UpdateUsers(List<Users> users)
        {
            foreach (Users u in users)
            {
                dgv.Rows.Add(u.GetData());
                AddRecord();
            }
        }
        public void EndRequest()
        {
            dgv.Invoke(new MethodInvoker(() => { hideGif(); }));
        }
        #endregion
        #region Functions
        private void showGif()
        {
            pictureBox4.Visible = pictureBox5.Visible = pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = pictureBox10.Visible = true;
        }
        private void hideGif()
        {
            pictureBox4.Visible = pictureBox5.Visible = pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = pictureBox10.Visible = false;
        }
        private void AddRecord(int count = -1)
        {
            if (count != -1)
                recordNumber = count;
            else
                recordNumber++;
            recordL.Text = string.Format("Record Number: {0}", recordNumber);
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
