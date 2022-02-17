using System.Collections.Generic;
using CoreUI;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Stats
{
    public class StatsChangerView : BaseView
    {
        [SerializeField] private List<StatView> _statViews;
        [SerializeField] private TMP_Text _freeStatsText;

        public List<StatView> StatViews => _statViews;
        public TMP_Text FreeStatsText => _freeStatsText;
    }
}