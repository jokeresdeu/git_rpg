using System;
using System.Collections.Generic;
using System.Linq;
using CoreUI;
using GamePlay;
using PlayerCreator.Specialization;
using UnityEngine;

namespace PlayerCreator.Stats
{
    public class StatChanger : IViewController
    {
        private readonly StatsChangerView _changerView;
        private List<StatViewData> _statViewsData;

        private int _freeStats;

        public StatChanger(StatsChangerView changerView)
        {
            _changerView = changerView;
        }

        public void Initialize(params object[] args)
        {
            if (args == null || args.Length < 1 || !args.Any(arg => arg is StatsModel))
            {
                throw new NullReferenceException($"There is no args for {nameof(StatChanger)}");
            }

            object model = args.First(arg => arg is StatsModel);
            StatsModel statsModel = model as StatsModel;

            _statViewsData = new List<StatViewData>();

            _freeStats = statsModel.FreeStats;
            _changerView.FreeStatsText.text = $"Stats left : {_freeStats}";

            for (int i = 0; i < statsModel.Stats.Count; i++)
            {
                if (i >= _changerView.StatViews.Count)
                {
                    break;
                }

                _changerView.StatViews[i].Initialize(statsModel.Stats[i].StatType.ToString());
                _changerView.StatViews[i].OnStatViewDecreaseClicked += DecreaseStatValue;
                _changerView.StatViews[i].OnStatViewIncreaseClicked += IncreaseStatValue;
                _changerView.StatViews[i].OnStatViewValueClicked += ChangeStatValue;
                _statViewsData.Add(new StatViewData(_changerView.StatViews[i], statsModel.Stats[i],
                    statsModel.Stats[i].Value));
            }

            UpdateStatViews();
            _changerView.Show();
        }

        public void Complete()
        {
            foreach (var statViewData in _statViewsData)
            {
                statViewData.StatView.Dispose();
                statViewData.StatView.OnStatViewDecreaseClicked -= DecreaseStatValue;
                statViewData.StatView.OnStatViewIncreaseClicked -= IncreaseStatValue;
                statViewData.StatView.OnStatViewValueClicked -= ChangeStatValue;
            }

            _changerView.Hide();
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
            _changerView.FreeStatsText.text = $"Stats left : {_freeStats}";
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