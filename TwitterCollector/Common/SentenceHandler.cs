using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Common
{
    public class SentenceHandler
    {
        public void SentenceSpliter(string sentence)
        {
            HashSet<string> subSentenceList = new HashSet<string>();
            string[] delimeters = { ", ", ". ", "; ", "! ", "? "};
            // Split sentence by delimeters

            // Sign quats values as sub sentence
            // Split all other sentence to sentence permutation
        }
        public void SearchEmoticons(ref string sentence)
        {
        }
    }
}
