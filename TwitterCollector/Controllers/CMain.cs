using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterCollector.Common;
using TwitterCollector.Forms;
using TwitterCollector.Interface;
using TwitterCollector.Objects;

namespace TwitterCollector.Controllers
{
    public class CMain : BaseController, UiUpdater
    {
        private Main form = new Main();

        public CMain()
        {
            form.SetController(this);
            LoadSubjectResult();
            Global.main = this;
            Global.CreateSettingController();
            _updateUiTimer.RegisterUpdater(this);
            form.ShowDialog();
        }

        #region UI Functions
        #endregion

        #region Functions
        private void LoadSubjectResult()
        {
            SubjectResultUI dashboardData = db.GetMainResults();
            form.LoadLastSubjectResults(dashboardData);
        }
        #endregion

        #region Implement Methods

        public override Form GetUI(UiState state)
        {
            if (state == UiState.SHOW)
                _updateUiTimer.RegisterUpdater(this);
            else
                _updateUiTimer.UnRegisterUpdater(this);
            return form;
        }

        public override Form GetUI()
        {
            return form;
        }

        public override void ToolStripAction(string buttonName)
        {
            Global.ToolStripAction(buttonName,this);
        }

        public void UpdateUi(UpdateType updateType, object updateObject)
        {
            if(updateType == UpdateType.MAIN)
                form.ReloadSubjectResults((SubjectResultUI)updateObject);
        }

        #endregion
    }
}
