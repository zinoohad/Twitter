using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Interface
{
    public interface GenericListener
    {
        void RegisterUpdater(GenericUpdater updater);

        void UnRegisterUpdater(GenericUpdater updater);
    }
}
