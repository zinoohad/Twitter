using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Classes
{
    public class AgeResult
    {
        [JsonProperty("age")]
        public int Age;

        [JsonProperty("freq")]
        public double Frequency;

        public int N;
    }

    public class AgeResultNavigator
    {
        [JsonProperty("ngram_json_array")]
        public List<List<AgeResult>> AgeResultList { get; set; }

        [JsonProperty("ngram_json_labels")]
        public List<string> WordsSet { get; set; }
    }

    /// <summary>
    /// This class will contains the return data from the api after calculations
    /// </summary>
    public class WordAge
    {
        public string Word;

        public double Age13To18;

        public double Age19To22;

        public double Age23To29;

        public double Age30Plus;

        public int MostPositiveAgeGroup;

        public int MostNegativeAgeGroup;


        public WordAge() { }

        public WordAge(string word, List<AgeResult> ageResultList)
        {
            Word = word;
            Age13To18 = ageResultList.Where(a => a.Age >= 13 && a.Age <= 18).Sum(s => s.Frequency)
                        / ageResultList.Where(a => a.Age >= 13 && a.Age <= 18).Count();

            Age19To22 = ageResultList.Where(a => a.Age >= 19 && a.Age <= 22).Sum(s => s.Frequency)
                        / ageResultList.Where(a => a.Age >= 19 && a.Age <= 22).Count();

            Age23To29 = ageResultList.Where(a => a.Age >= 23 && a.Age <= 29).Sum(s => s.Frequency)
                        / ageResultList.Where(a => a.Age >= 23 && a.Age <= 29).Count();

            Age30Plus = ageResultList.Where(a => a.Age >= 30).Sum(s => s.Frequency)
                        / ageResultList.Where(a => a.Age >= 30).Count();

            double MaxValue = Math.Max(Math.Max(Math.Max(Age13To18, Age19To22), Age23To29), Age30Plus);

            double MinValue = Math.Min(Math.Min(Math.Min(Age13To18, Age19To22), Age23To29), Age30Plus);

            if( MaxValue == Age13To18)
                MostPositiveAgeGroup = 1;
            else if (MaxValue == Age19To22)
                MostPositiveAgeGroup = 2;
            else if (MaxValue == Age23To29)
                MostPositiveAgeGroup = 3;
            else if (MaxValue == Age30Plus)
                MostPositiveAgeGroup = 4;

            if (MinValue == Age13To18)
                MostNegativeAgeGroup = 1;
            else if (MinValue == Age19To22)
                MostNegativeAgeGroup = 2;
            else if (MinValue == Age23To29)
                MostNegativeAgeGroup = 3;
            else if (MinValue == Age30Plus)
                MostNegativeAgeGroup = 4;
            
        }

    }

    public class WordGender
    {
        public string Word;

        public double WordRate;
    }

}
