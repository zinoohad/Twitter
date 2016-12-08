using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class Segment
    {
        public string text { get; set; }
        public string segment_type { get; set; }
        public int inip { get; set; }
        public int endp { get; set; }
        public int confidence { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public IList<PolarityTerm> polarity_term_list { get; set; }
        public IList<Entity> sentimented_entity_list { get; set; }
    }
}
