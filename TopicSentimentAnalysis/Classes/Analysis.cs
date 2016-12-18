using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class Analysis
    {
        public Status status { get; set; }
        public string model { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public string subjectivity { get; set; }
        public int confidence { get; set; }
        public string irony { get; set; }
        public IList<PolarityTerm> polarity_term_list { get; set; }
        public IList<Sentence> sentence_list { get; set; }
        public IList<Entity> sentimented_entity_list { get; set; }
        public IList<Concept> sentimented_concept_list { get; set; }
    }
}
