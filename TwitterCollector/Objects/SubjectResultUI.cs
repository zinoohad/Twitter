using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Objects
{
    public class SubjectResultUI
    {
        public int SubjectID = -1;

        public bool SwitchOn = false;

        public int TotalTweets = 0;

        public int TotalUsers = 0;

        public DateTime StartTime;

        public DataTable KeywordsDT;

        public Dictionary<string, double> AgeDivision;

        public Dictionary<string, double> GenderDivision;

        public List<string> Subjects;

        public Dictionary<string, List<KeywordO>> SubjectKeywordsTweetCount;
    }
}
