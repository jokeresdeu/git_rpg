using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace PlayerCreator.Stats
{
    public class StatChanger
    {
        private readonly StatsChangerView _view;
        
        private List<StatViewData> _statViewsData;
        private int _freeStats;
        
        public StatChanger(StatsChangerView view)
        {
            _view = view;
            
        }

        public void Initialize(StatsModel statsModel)
        {
            //List<Stat> stats = statsModel.Stats
            //_freeStats = statsModel.FreeStats;
            _statViewsData = new List<StatViewData>();
            
            List<Stat> stats = new List<Stat>
                {new Stat(StatType.Agility, 2), new Stat(StatType.Intelligence, 1), new Stat(StatType.Strength, 1)};
            _freeStats = 10;
            
            for (int i = 0; i < stats.Count; i++)
            {
                if (i >=_view.StatViews.Count)
                {
                    break;
                }

                _view.StatViews[i].Initialize(stats[i].StatType.ToString());
                _view.StatViews[i].OnStatViewDecreaseClicked += DecreaseStatValue;
                _view.StatViews[i].OnStatViewIncreaseClicked += IncreaseStatValue;
                _view.StatViews[i].OnStatViewValueClicked += ChangeStatValue;
                _statViewsData.Add(new StatViewData(_view.StatViews[i], stats[i], stats[i].Value));
            }
            UpdateStatViews();
        }
        

        private void IncreaseStatValue(StatView statView)
        {
            StatViewData statViewData = _statViewsData.Find(data => data.StatView == statView);
            ChangeStat(statViewData, statViewData.Stat.Value + 1);
        }

        private void DecreaseStatValue(StatView statView)
        {
            StatViewData statViewData = _statViewsData.Find(data => data.StatView == statView);
            ChangeStat(statViewData, statViewData.Stat.Value - 1);
        }

        private void ChangeStatValue(StatView statView, int value)
        {
            StatViewData statViewData = _statViewsData.Find(data => data.StatView == statView);
            ChangeStat(statViewData, value);
        }

        private void ChangeStat(StatViewData statViewData, int value)
        {
            int oldValue = statViewData.Stat.Value;
            if (_freeStats < 0 && value > oldValue)
            {
                return;
            }

            if (value < statViewData.MinValue)
            {
                return;
            }

            value = Mathf.Clamp(value, statViewData.MinValue, oldValue + _freeStats);
            _freeStats += oldValue - value;
            _view.FreeStatsText.text = $"Stats left : {_freeStats}";
            statViewData.Stat.SetValue(value);
            UpdateStatViews();
        }

        private void UpdateStatViews()
        {
            foreach (var statViewData in _statViewsData)
            {
                int value = statViewData.Stat.Value;
                statViewData.StatView.UpdateView(_freeStats > 0 && value < statViewData.StatView.MaxValue,
                    value > statViewData.MinValue, value);
            }
        }
    }
}