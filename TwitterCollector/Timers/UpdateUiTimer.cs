using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCollector.Common;
using TwitterCollector.Interface;
using TwitterCollector.Objects;

namespace TwitterCollector.Timers
{
    public class UpdateUiTimer : GenericTimer, GenericListener
    {
        private List<UiUpdater> updatersList = new List<UiUpdater>();

        private DBHandler db = Global.DB;

        public UpdateUiTimer() : base()
        { 
        }

        public void RegisterUpdater(GenericUpdater updater)
        {
            updatersList.Add((UiUpdater)updater);
            if (updatersList.Count == 1)
            {
                int timerIntervals = int.Parse(db.GetValueByKey("UpdateUiIntervalsInSeconds",5).ToString());
                SetTimer(timerIntervals);
            }
        }

        public void UnRegisterUpdater(GenericUpdater updater)
        {
            updatersList.Remove((UiUpdater)updater);
            if (updatersList.Count == 0)
                StopTimer();
        }

        protected override void TimerIntervalEnd()
        {
            // Update all UIs
            foreach (UiUpdater updater in updatersList)
            {
                updater.UpdateUi(UpdateType.MAIN, db.GetMainResults());
                //updater.UpdateUi(UpdateType.SUBJECT_RESULTS, new object());
            }
        }
    }
}
