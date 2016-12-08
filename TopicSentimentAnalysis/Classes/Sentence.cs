using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class Sentence
    {
        public string text { get; set; }
        public int inip { get; set; }
        public int endp { get; set; }
        public string bop { get; set; }
        public int confidence { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public IList<Segment> segment_list { get; set; }
        public IList<Entity> sentimented_entity_list { get; set; }
        public IList<Concept> sentimented_concept_list { get; set; }
    }
}
