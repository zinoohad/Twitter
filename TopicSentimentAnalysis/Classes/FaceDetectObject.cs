using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class FaceDetectObject
    {
        public IList<FImage> images { get; set; }
        public int images_processed { get; set; }
    }
    public class Age
    {
        public int max { get; set; }
        public int min { get; set; }
        public double score { get; set; }
    }

    public class FaceLocation
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }

    public class Gender
    {
        public string gender { get; set; }
        public double score { get; set; }
    }

    public class Face
    {
        public Age age { get; set; }
        public FaceLocation face_location { get; set; }
        public Gender gender { get; set; }
    }

    public class FImage
    {
        public IList<Face> faces { get; set; }
        public string resolved_url { get; set; }
        public string source_url { get; set; }
        public IList<Classifier> classifiers { get; set; }
        public ApiError error { get; set; }
    }

    public class ApiError
    {
        public string description { get; set; }
        public string error_id { get; set; }
    }
}
