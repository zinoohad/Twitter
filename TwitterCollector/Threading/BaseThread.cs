﻿using System;
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
        protected DBHandler db = new DBHandler();
        protected TwitterAPI twitter = new TwitterAPI();
        protected BaseThread() { _thread = new Thread(new ThreadStart(this.RunThread)); }

        // Thread methods / properties
        public void Start() { _thread.Start(); }
        public void Join() { _thread.Join(); }
        public bool IsAlive { get { return _thread.IsAlive; } }

        // Override in base class
        public abstract void RunThread();
        public virtual void SetInitialParams(params object[] Params) { }
    }
}
