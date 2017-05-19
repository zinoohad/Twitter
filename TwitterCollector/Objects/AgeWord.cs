using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Objects
{
    public class AgeWord
    {
        public int WORDS_COUNT = 53;
        public List<Double> WordRates { get; set; }
        public AgeWord()
        {
           WordRates = Enumerable.Repeat<double>(0, WORDS_COUNT).ToList();
        }

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }
    }
}
