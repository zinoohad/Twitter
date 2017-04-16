using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class ImaggaObject
    {
        public IList<Result> results { get; set; }
    }
    public class Tag
    {
        public double confidence { get; set; }
        public string tag { get; set; }
    }

    public class Result
    {
        public object tagging_id { get; set; }
        public string image { get; set; }
        public IList<Tag> tags { get; set; }
    }


    public class FImaggaObject
    {
        public IList<Tagss> tagss { get; set; }
        public string image { get; set; }
        public string aws { get; set; }
    }

    public class Tagss
    {
        public string namee { get; set; }
        public string confidence { get; set; }
    }
    
}
