using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Objects
{
    class WordClassification
    {
        List<WordProperties> wordsList;
        public class WordProperties
        {
            public WordProperties(string wordstring, double value,int age)
            {
                word = wordstring;
                repeatCount = 1;
                wordValue = value;
                AgeGroup = age;

            }
            public string word { get; set; }
            public int repeatCount { get; set; }
            public double wordValue { get; set; }
            public int AgeGroup { get; set; }
        }
        public WordClassification() { wordsList = new List<WordProperties>(); }
        
        public void IncreaseWord(string word, double wordValue,int age)
        {
            if (wordsList.Any(item => item.word.Equals(word)))
            {
                WordProperties wp = wordsList.Single(item => item.word.Equals(word));
                wp.repeatCount++;
            }
            else
            {
                wordsList.Add(new WordProperties(word, wordValue,age));
            }
        }
        public void print()
        {
             foreach (WordProperties word in wordsList)
            {
                Console.WriteLine("key  {0}: repeat = {1} , value = {2} AgeGroup = {3}", word.word, word.repeatCount, word.wordValue, word.AgeGroup);
                
            }
        }

    }
}
