using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Threading;

namespace TwitterCollector.Objects
{
    public class GeneralThreadParams
    {
        private DateTime _startTime = DateTime.Now;
        public DateTime StartTime { get { return _startTime; } }

        private int _subjectID;
        public int SubjectID { get { return _subjectID; } }

        private BaseThread _thread;
        public BaseThread Thread { get { return _thread; } }

        private string _name;
        public string Name { get { return _name; } }

        public GeneralThreadParams() { }
        public GeneralThreadParams(int subjectID, BaseThread thread)
        {
            this._subjectID = subjectID;
            this._thread = thread;
            this._name = thread.GetType().Name;
        }

        public bool SetBaseThread(BaseThread thread)
        {
            if (thread.GetType() != _thread.GetType()) return false;
            _thread = thread;
            return true;
        }
    }
}
