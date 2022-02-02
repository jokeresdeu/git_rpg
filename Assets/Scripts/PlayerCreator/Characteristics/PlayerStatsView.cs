using System.Collections.Generic;
using GamePlay;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Characteristics
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private List<StatView> _statsViews;
        [SerializeField] private TMP_Text _freeStatsValue;

        private List<StatView> _activeStatViews;
        private int _freeStats;

        private List<StatViewData> _stats;
        private void Start()
        {
            _stats = new List<StatViewData>();
            var stats = new List<Stat> {new Stat(StatType.Agility, 2), new Stat(StatType.Intelligence, 1), new Stat(StatType.Strength, 2)};
            _freeStats = 10;
            _freeStatsValue.text = $"Stats left : {_freeStats.ToString()}";
            _activeStatViews = new List<StatView>();
            for (int i = 0; i < stats.Count; i++)
            {
                _statsViews[i].Initialize(stats[i].StatType);
                _statsViews[i].OnStatViewValueChanged += ChangeStatValue;
                _statsViews[i].OnStatViewValueDecreased += DecreaseStatValue;
                _statsViews[i].OnStatViewValueIncreased += IncreaseStatValue;
                _stats.Add(new StatViewData(stats[i], stats[i].Value, _statsViews[i].MaxValue));
                _activeStatViews.Add(_statsViews[i]);
            }
        }

        private void DecreaseStatValue(StatType statType)
        {
            var statViewData = _stats.Find(stat => stat.Stat.StatType == statType);
            if (statViewData.Stat.Value - 1 < statViewData.MinValue)
            {
                return;
            }
            ChangeStatValue(statViewData, statViewData.Stat.Value -1);
        }

        private void IncreaseStatValue(StatType statType)
        {
            var statViewData = _stats.Find(stat => stat.Stat.StatType == statType);
            if (statViewData.Stat.Value + 1 > statViewData.MaxValue)
            {
                return;
            }
            ChangeStatValue(statViewData, statViewData.Stat.Value + 1);
        }

        private void ChangeStatValue(StatType statType, int value)
        {
            var statViewData = _stats.Find(stat => stat.Stat.StatType == statType);
            value = Mathf.Clamp(value, statViewData.MinValue, statViewData.MaxValue);
            ChangeStatValue(statViewData, value);
        }

        private void ChangeStatValue(StatViewData statViewData, int statValue)
        {
            statViewData.Stat.SetValue(statValue);
            foreach (var statView in _activeStatViews)
            {
                var statViewData = _stats.Find(data => data.Stat.StatType == statView.S)
            }
        }

        /*private void OnStatChanged(StatView statView)
        {
            _activeStatViews.TryGetValue(statView, out StatType statType);
            Stat changedStat = _stats.Find(stat => stat.StatType == statType);
            int diff = changedStat.Value - statView.StatValue;
            changedStat.SetValue(statView.StatValue);
            _freeStats += diff;
            _freeStatsValue.text = $"Stats left : {_freeStats.ToString()}";
            foreach (var key in _activeStatViews.Keys)
            {
                key.Update(_freeStats > 0);
            }
        }*/
    }
}