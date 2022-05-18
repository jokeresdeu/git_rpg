using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.PlayerCreator.Stats
{
    public class StatsChangerView : BaseView
    {
        [SerializeField] private StatView _statView;
        [SerializeField] private Transform _statViewContainer;
        [SerializeField] private TMP_Text _freeStatsText;

        public StatView StatView => _statView;
        public Transform StatViewContainer => _statViewContainer;
        public TMP_Text FreeStatsText => _freeStatsText;
    }
}