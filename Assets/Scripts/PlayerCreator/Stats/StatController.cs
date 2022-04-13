using System;

namespace PlayerCreator.Stats
{
    public class StatController
    {
        private readonly StatView _statView;
        private bool _canIncrease;

        public event Action<StatController> OnStatIncreased;
        public event Action<StatController> OnStatDecreased;
        public event Action<StatController, int> OnStatValueChanged;

        public StatController(StatView statView, string statText)
        {
            _statView = statView;
            _statView.SetHeader(statText);
            _statView.OnIncreased += IncreaseStat;
            _statView.OnDecreased += DecreaseStat;
            _statView.OnStatValueChanged += StatValueChanged;
        }

        public void Dispose()
        {
            _statView.OnIncreased -= IncreaseStat;
            _statView.OnDecreased -= DecreaseStat;
            _statView.OnStatValueChanged -= StatValueChanged;
            _statView.Reset();
        }

        private void IncreaseStat()
        {
            if (!_canIncrease)
            {
                return;
            }

            OnStatIncreased?.Invoke(this);
        }

        private void DecreaseStat()
        {
            OnStatDecreased?.Invoke(this);
        }

        private void StatValueChanged(int value)
        {
            OnStatValueChanged?.Invoke(this, value);
        }

        private void SetButtonsState(int value)
        {
            foreach (var statButton in _statView.StatsButtons)
            {
                statButton.SetState(_statView.StatsButtons.IndexOf(statButton) < value);
            }
        }

        public void UpdateView(bool canIncrease, bool canDecrease, int value)
        {
            _canIncrease = canIncrease;
            _statView.SetDecreaseStatus(canDecrease);
            ChangeStat(value);
        }

        private void ChangeStat(int statValue)
        {
            _statView.SetValue(statValue);
            SetButtonsState(statValue);
        }
    }
}