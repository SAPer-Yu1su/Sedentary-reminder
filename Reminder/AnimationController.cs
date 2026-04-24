using System;
using System.Windows.Forms;

namespace Reminder
{
    public class AnimationController : IDisposable
    {
        private Form _targetForm;
        private Timer _fadeTimer;
        private Timer _breatheTimer;
        private int _breathePhase = 0;

        public int BreathePhase => _breathePhase;

        public event EventHandler AnimationTick;

        public AnimationController(Form targetForm)
        {
            _targetForm = targetForm;
        }

        public void StartFadeIn(double targetOpacity, int intervalMs = 30)
        {
            if (_fadeTimer != null) return;
            _targetForm.Opacity = 0;
            _fadeTimer = new Timer { Interval = intervalMs };
            _fadeTimer.Tick += (s, e) =>
            {
                if (_targetForm.Opacity < targetOpacity)
                    _targetForm.Opacity += 0.06;
                else
                {
                    _fadeTimer.Stop();
                    _fadeTimer.Dispose();
                    _fadeTimer = null;
                }
            };
            _fadeTimer.Start();
        }

        public void StartPulse()
        {
            if (_breatheTimer != null) return;
            _breatheTimer = new Timer { Interval = 80 };
            _breatheTimer.Tick += (s, e) =>
            {
                _breathePhase = (_breathePhase + 1) % 100;
                AnimationTick?.Invoke(this, EventArgs.Empty);
            };
            _breatheTimer.Start();
        }

        public void StopPulse()
        {
            if (_breatheTimer != null)
            {
                _breatheTimer.Stop();
                _breatheTimer.Dispose();
                _breatheTimer = null;
            }
            _breathePhase = 0;
        }

        public void Dispose()
        {
            if (_fadeTimer != null)
            {
                _fadeTimer.Stop();
                _fadeTimer.Dispose();
            }
            if (_breatheTimer != null)
            {
                _breatheTimer.Stop();
                _breatheTimer.Dispose();
            }
        }
    }
}

