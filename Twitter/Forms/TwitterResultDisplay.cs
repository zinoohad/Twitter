using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitter.Classes;
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

        private void GoB_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Name)
            {
                case "t5GoB":
                    if (string.IsNullOrEmpty(t5KeywordsTB.Text))
                    {
                        return;
                    }
                    string lang = t5LangCB.SelectedItem.Equals("Hebrew") ? "he" : "en";
                    string resultType = t5ResultCB.SelectedItem.Equals("Recent") ? "recent" : t5ResultCB.SelectedItem.Equals("Popular") ? "popular" : "mixed";
                    string[] headers = new string[]{"ID","Create Date","Tweet","Language","Retweet Number","Hashtags"};
                    SetDGV(headers);
                    tweets = twitter.SearchTweets(t5KeywordsTB.Text, (int)t5MaxTweetsUD.Value, (int)t5TweetsPerPageUD.Value, lang, resultType, t5IncEntCB.Checked,this).Result;
                    
                    break;

            }
        }

        public void UpdateTweets(List<Tweets> tweets)
        {
            foreach (Tweets t in tweets)
                dgv.Rows.Add(t.GetData());

        }


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
