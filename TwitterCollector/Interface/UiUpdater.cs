using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;

namespace TwitterCollector.Interface
{
    public interface UiUpdater : GenericUpdater
    {
        void UpdateUi(UpdateType updateType, object updateObject);
    }
}
