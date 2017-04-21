using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSentimentAnalysis.Interfaces
{
    public interface Update
    {
        void Update(object obj);
        void EndRequest();
    }
}
