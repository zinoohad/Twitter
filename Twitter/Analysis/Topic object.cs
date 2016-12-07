using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Analysis
{

    public class Topicobject
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
