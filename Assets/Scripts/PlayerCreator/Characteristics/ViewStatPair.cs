using System;
using GamePlay;
using UnityEngine;

namespace PlayerCreator.Characteristics
{
    [Serializable]
    public class ViewStatPair
    {
        [SerializeField] private StatView _statView;
        [SerializeField] private StatType _statType;
        
        public StatView StatView => _statView;
        public StatType StatType => _statType;
    }
}