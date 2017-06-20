using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitterCollector.Timers
{
    public abstract class GenericTimer
    {

        private Timer _timer;

        private int _mainThreadID = Process.GetCurrentProcess().Id;

        protected object _localLocker = new object();

        protected static object _globalLocker = new object();

        public GenericTimer() { }

        public GenericTimer(int intervalInSeconds)
        {
            SetTimer(intervalInSeconds);
        }

        #region Timer Functionality

        /// <summary>
        /// Start new timer on background.
        /// The timer restart over and over again, to cancel the timer set 'timerTime' to zero.
        /// </summary>
        /// <param name="seconds">Time in seconds until the buffer will be clear.</param>
        protected void SetTimer(int seconds)
        {
            if (_timer != null) _timer.Close(); // Cancel previous timer

            _timer = new System.Timers.Timer(seconds * 1000);
            _timer.Elapsed += new ElapsedEventHandler(TimerEnd);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        protected void TimerEnd(object sender, ElapsedEventArgs e)
        {
            if (!MainThreadIsAlive())
                _timer.Close();
            TimerIntervalEnd();
        }

        protected void StopTimer() { _timer.Close(); }

        protected abstract void TimerIntervalEnd();

        private bool MainThreadIsAlive()
        {
            try
            {
                Process p = Process.GetProcessById(_mainThreadID);
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected void AutoReset(bool autoReset)
        {
            _timer.AutoReset = autoReset;
        }

        #endregion
        
    }
}
