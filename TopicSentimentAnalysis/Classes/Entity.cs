using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class Entity
    {
        public string form { get; set; }
        public string id { get; set; }
        public string variant { get; set; }
        public int inip { get; set; }
        public int endp { get; set; }
        public string type { get; set; }
        public string score_tag { get; set; }
    }
}
