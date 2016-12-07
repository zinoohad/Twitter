using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Analysis
{
    public class Status
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string credits { get; set; }
    }

    public class SentimentedConceptList
    {
        public string form { get; set; }
        public string id { get; set; }
        public string variant { get; set; }
        public string inip { get; set; }
        public string endp { get; set; }
        public string type { get; set; }
        public string score_tag { get; set; }
    }

    public class PolarityTermList
    {
        public string text { get; set; }
        public string inip { get; set; }
        public string endp { get; set; }
        public string confidence { get; set; }
        public string score_tag { get; set; }
        public IList<SentimentedConceptList> sentimented_concept_list { get; set; }
    }

    public class SentimentedEntityList
    {
        public string form { get; set; }
        public string id { get; set; }
        public string variant { get; set; }
        public string inip { get; set; }
        public string endp { get; set; }
        public string type { get; set; }
        public string score_tag { get; set; }
    }

    public class SegmentList
    {
        public string text { get; set; }
        public string segment_type { get; set; }
        public string inip { get; set; }
        public string endp { get; set; }
        public string confidence { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public IList<PolarityTermList> polarity_term_list { get; set; }
        public IList<SentimentedEntityList> sentimented_entity_list { get; set; }
    }


    public class SentenceList
    {
        public string text { get; set; }
        public string inip { get; set; }
        public string endp { get; set; }
        public string bop { get; set; }
        public string confidence { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public IList<SegmentList> segment_list { get; set; }
        public IList<SentimentedEntityList> sentimented_entity_list { get; set; }
        public IList<SentimentedConceptList> sentimented_concept_list { get; set; }
    }

    

    public class Sentence_object
    {
        public Status status { get; set; }
        public string model { get; set; }
        public string score_tag { get; set; }
        public string agreement { get; set; }
        public string subjectivity { get; set; }
        public string confidence { get; set; }
        public string irony { get; set; }
        public IList<SentenceList> sentence_list { get; set; }
        public IList<SentimentedEntityList> sentimented_entity_list { get; set; }
        public IList<SentimentedConceptList> sentimented_concept_list { get; set; }
    }
}
