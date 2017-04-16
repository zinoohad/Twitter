using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using System.Timers;
using System.Diagnostics;

namespace TwitterCollector.Timers
{
    public class NewAgeWordsTimer
    {

        #region Params

        private DBHandler db = Global.DB;

        private List<string> _sentencesBuffer = new List<string>();

        private object _locker = new object();

        private Timer _timer;

        private int _maxWordsInSubSentence;

        private int _mainThreadID = Process.GetCurrentProcess().Id;

        private int _timerInterval = 0;
        /// <summary>
        /// The timer interval in seconds.
        /// </summary>
        public int TimerInterval
        {
            set
            {
                if (value > 0)
                {
                    _timerInterval = value;
                    SetTimer(value);    //Start timer
                }
                else if (_timer != null)
                    _timer.Close(); //Stop timer
            }
        }


        #endregion

        public NewAgeWordsTimer()
        {
            _maxWordsInSubSentence = int.Parse(db.GetValueByKey("MaxWordInSubSentence",3).ToString());
        }

        #region Timer Functionality

        /// <summary>
        /// Start new timer on background.
        /// The timer restart over and over again, to cancel the timer set 'timerTime' to zero.
        /// </summary>
        /// <param name="seconds">Time in seconds until the buffer will be clear.</param>
        private void SetTimer(int seconds)
        {
            if (_timer != null) _timer.Close(); // Cancel previous timer

            _timer = new System.Timers.Timer(seconds * 1000);
            _timer.Elapsed += new ElapsedEventHandler(TimerEnd);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void TimerEnd(object sender, ElapsedEventArgs e)
        {
            if (!MainThreadIsAlive())
                _timer.Close();
            ReadBuffer();
        }

        #endregion

        public void Add(params string[] values)
        {
            if (values == null || values.Length == 0)
                return; 

            lock (_locker)
            {
                _sentencesBuffer.AddRange(values);
            }
        }

        private void ReadBuffer()
        {
            List<string> localBuffer;
            lock (_locker)
            {
                localBuffer = _sentencesBuffer;
                _sentencesBuffer.Clear();
            }
            foreach (string s in localBuffer)
                Global.LearnNewWordsToAgeDictionary(s, _maxWordsInSubSentence);
        }

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
    }
}
