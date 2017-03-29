using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Objects
{
    public class PosNegTweet
    {
        public string ID;

        public int PositiveEmoticons;

        public int NegativeEmoticons;

        public int PositiveWords;

        public int NegativeWords;

        public int LocalRank;

        public string ApiRank;

        public int ApiConfidence;

        public void CalculateRank()
        {
            LocalRank = (PositiveEmoticons + PositiveWords) - (NegativeEmoticons + NegativeWords);
        }

        public void Clear()
        {
            PositiveWords = PositiveEmoticons = NegativeWords = NegativeEmoticons = LocalRank = ApiConfidence = 0;
            ID = ApiRank = null;
        }

        public int GetPositive()
        {
            return PositiveEmoticons + PositiveWords;
        }
        public int GetNegative()
        {
            return NegativeEmoticons + NegativeWords;
        }

    }
}
