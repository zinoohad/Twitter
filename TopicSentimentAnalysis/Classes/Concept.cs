using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class Concept : Entity
    {
        public bool NotIn(params string[] scoreTag)
        {
            if (score_tag.Contains(this.score_tag))
                return false;
            return true;
        }

        public bool In(params string[] scoreTag)
        {
            if (score_tag.Contains(this.score_tag))
                return true;
            return false;
        }
    }
}
