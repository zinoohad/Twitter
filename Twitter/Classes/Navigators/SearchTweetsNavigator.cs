using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Classes.Navigators
{
    class SearchTweetsNavigator
    {
        public IList<Tweets> statuses { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }
}
