using System;

namespace Reminder
{
    public enum SessionState { Idle, Working, Resting }

    public class SessionManager
    {
        private static SessionManager _instance;
        private SessionState _currentState = SessionState.Idle;
        private WorkFrm _workForm;
        private RestFrm _restForm;
        private SessionConfig _config;

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();
                return _instance;
            }
        }

        private SessionManager() { }

        public SessionState CurrentState => _currentState;

        public void StartWorkSession(SessionConfig config)
        {
            _config = config;
            _currentState = SessionState.Working;
            _workForm = new WorkFrm(config);
            _workForm.Show();
        }

        public void TransitionToRest()
        {
            if (_workForm != null)
            {
                _workForm.Hide();
                _workForm.Close();
                _workForm = null;
            }

            _currentState = SessionState.Resting;
            TrayManager.Instance.UpdateTooltip("休息时间到！");
            _restForm = new RestFrm(_config);
            _restForm.Show();
        }

        public void TransitionToWork()
        {
            if (_restForm != null)
            {
                _restForm.Hide();
                _restForm.Close();
                _restForm = null;
            }

            ConfigManager.IncrementStatistics(_config.WorkMinutes);

            _currentState = SessionState.Working;
            _workForm = new WorkFrm(_config);
            _workForm.Show();
        }

        public void StopSession()
        {
            if (_workForm != null)
            {
                _workForm.Close();
                _workForm = null;
            }
            if (_restForm != null)
            {
                _restForm.Close();
                _restForm = null;
            }
            _currentState = SessionState.Idle;
            TrayManager.Instance.UpdateTooltip("久坐提醒");
        }
    }
}

