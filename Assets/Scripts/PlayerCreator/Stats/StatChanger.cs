using System;
using System.Collections.Generic;
using System.Linq;
using CoreUI;
using ObjectPooling;
using UnityEngine;

namespace PlayerCreator.Stats
{
    public class StatChanger : IViewController
    {
        private const int MaxStatValue = 10;
        private readonly StatsChangerView _changerView;
        private List<StatControllerData> _statControllerViewsData;

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

            _statControllerViewsData = new List<StatControllerData>();

            _freeStats = statsModel.FreeStats;
            _changerView.FreeStatsText.text = $"Stats left : {_freeStats}";

            for (int i = 0; i < statsModel.Stats.Count; i++)
            {
                StatView view = ObjectPool.Instance.GetObject(_changerView.StatView);
                view.Transform.parent = _changerView.StatViewContainer;
                view.Transform.localScale = Vector3.one;
                StatController statController = new StatController(view, statsModel.Stats[i].StatType.ToString());
                statController.OnStatDecreased += DecreaseStatValue;
                statController.OnStatIncreased += IncreaseStatValue;
                statController.OnStatValueChanged += ChangeStatValue;
                _statControllerViewsData.Add(new StatControllerData(statController, statsModel.Stats[i],
                    statsModel.Stats[i].Value));
            }

            UpdateStatViews();
            _changerView.Show();
        }

        public void Complete()
        {
            foreach (var statViewData in _statControllerViewsData)
            {
                statViewData.StatController.Dispose();
                statViewData.StatController.OnStatDecreased += DecreaseStatValue;
                statViewData.StatController.OnStatIncreased += IncreaseStatValue;
                statViewData.StatController.OnStatValueChanged += ChangeStatValue;
            }

            _changerView.Hide();
        }

        private void IncreaseStatValue(StatController statController)
        {
            StatControllerData statControllerData = _statControllerViewsData.Find(data => data.StatController == statController);
            ChangeStat(statControllerData, statControllerData.Stat.Value + 1);
        }

        private void DecreaseStatValue(StatController statController)
        {
            StatControllerData statControllerData = _statControllerViewsData.Find(data => data.StatController == statController);
            ChangeStat(statControllerData, statControllerData.Stat.Value - 1);
        }

        private void ChangeStatValue(StatController statController, int value)
        {
            StatControllerData statControllerData = _statControllerViewsData.Find(data => data.StatController == statController);
            ChangeStat(statControllerData, value);
        }

        private void ChangeStat(StatControllerData statControllerData, int value)
        {
            int oldValue = statControllerData.Stat.Value;
            if (_freeStats < 0 && value > oldValue)
            {
                return;
            }

            if (value < statControllerData.MinValue)
            {
                return;
            }

            value = Mathf.Clamp(value, statControllerData.MinValue, oldValue + _freeStats);
            _freeStats += oldValue - value;
            _changerView.FreeStatsText.text = $"Stats left : {_freeStats}";
            statControllerData.Stat.SetValue(value);
            UpdateStatViews();
        }

        private void UpdateStatViews()
        {
            foreach (var statViewData in _statControllerViewsData)
            {
                int value = statViewData.Stat.Value;
                statViewData.StatController.UpdateView(_freeStats > 0 && value < MaxStatValue,
                    value > statViewData.MinValue, value);
            }
        }
    }
}