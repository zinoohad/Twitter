﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes
{
    public class Place : PlaceBase
    {
        public List<PlaceBase> contained_within { get; set; }
        public Geometry geometry { get; set; }
        public IList<string> polylines { get; set; }
    }

    public class PlaceBase
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

    public class Geometry 
    {
        public IList<IList<IList<double>>> coordinates { get; set; }
        public string type { get; set; }
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

    public class GeoParams
    {
        public decimal Latitude;

        public decimal Longitude;

        public float Radius;

        public RadiusUnits radiusUnits = RadiusUnits.km;

        public enum RadiusUnits
        {
            km, // kilometers
            mi  // miles
        }

        public string GetLocationForSearchAPI()
        {
            return string.Format("{0},{1},{3}" + radiusUnits.ToString(),Latitude,Longitude,Radius);
        }
    }
}
