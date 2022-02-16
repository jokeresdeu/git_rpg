using System.Collections.Generic;
using GamePlay;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Stats
{
    public class StatsView : MonoBehaviour //StatsView / StatsChanger
    {
        [SerializeField] private List<StatView> _statViews;
        [SerializeField] private TMP_Text _freeStatsText;

        private int _freeStats;

        private List<StatViewData> _statViewsData;

        private void Start()
        {
            List<Stat> stats = new List<Stat>
                {new Stat(StatType.Agility, 2), new Stat(StatType.Intelligence, 1), new Stat(StatType.Strength, 1)};
            _statViewsData = new List<StatViewData>();
            _freeStats = 10;
            _freeStatsText.text = $"Stats left : {_freeStats}";
            for (int i = 0; i < stats.Count; i++)
            {
                if (i >= _statViews.Count)
                {
                    break;
                }

                _statViews[i].Initialize(stats[i].StatType.ToString());
                _statViews[i].OnStatViewDecreaseClicked += DecreaseStatValue;
                _statViews[i].OnStatViewIncreaseClicked += IncreaseStatValue;
                _statViews[i].OnStatViewValueClicked += ChangeStatValue;
                _statViewsData.Add(new StatViewData(_statViews[i], stats[i], stats[i].Value));
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
            _freeStatsText.text = $"Stats left : {_freeStats}";
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