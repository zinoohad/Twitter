using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Common
{
    public enum ThreadType
    {
        SENTIMENT_ANALYSIS
        ,TWEET_POS_NEG
        ,NEW_WORD_COLLECTOR
        ,TWEETS_COLLECTOR
        ,USER_CLASSIFICATION
        ,USERS_COLLECTOR
        ,TWEET_AGE
        ,TWEET_GENDER
    }
    public enum Gender
    {
        MALE = 1, 
        FEMALE = 2,
        Unknown = 3
    }
}                                                                                                                                                                                                                                                                                                                                                                                     