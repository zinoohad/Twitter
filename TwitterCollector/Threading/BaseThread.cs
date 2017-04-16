using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twitter;
using TwitterCollector.Common;

namespace TwitterCollector.Threading
{
    public abstract class BaseThread
    {
        private Thread _thread;

        protected DBHandler db = Global.DB;

        protected TwitterAPI twitter;

        protected bool ThreadOn = false;

        protected Thread streamThread;

        protected BaseThread() 
        { 
            _thread = new Thread(new ThreadStart(this.RunThread));
            twitter = new TwitterAPI(db.GetTwitterKey());
        }

        // Thread methods / properties
        public void Start() { ThreadOn = true;  _thread.Start(); }

        public void Join() { _thread.Join(); }

        public bool IsAlive { get { return _thread.IsAlive; } }

        public virtual void Abort() { ThreadOn = false; _thread.Abort(); }

        // Override in base class
        public abstract void RunThread();

        public virtual void SetInitialParams(params object[] Params) { }

        #region Extension methods

        public bool In<T>(T obj, params T[] args)
        {
            return args.Contains(obj);
        }

        public bool NotIn<T>(T obj, params T[] args)
        {
            return !args.Contains(obj);
        }

        #endregion
    }
}
