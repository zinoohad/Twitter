using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class Places
    {
        public PlaceAttributes attributes { get; set; }
        public BoundingBox bounding_box { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string full_name { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string place_type { get; set; }
        public string url { get; set; }
    }
    public class PlaceAttributes
    {
        public object obj { get; set; }
    }
    public class BoundingBox
    {
        public IList<IList<IList<float>>> coordinates { get; set; }
        public string type { get; set; }
    }
}
