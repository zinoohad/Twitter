using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class ImageAnalysisObject
    {
        public int custom_classes { get; set; }
        public IList<Image> images { get; set; }
        public int images_processed { get; set; }
    }

    public class Class
    {
        [JsonProperty("class")]
        public string category { get; set; }
        public double score { get; set; }
    }

    public class Classifier
    {
        public IList<Class> classes { get; set; }
        public string classifier_id { get; set; }
        public string name { get; set; }
    }

    public class Image
    {
        public IList<Classifier> classifiers { get; set; }
        public string resolved_url { get; set; }
        public string source_url { get; set; }
    }
}
