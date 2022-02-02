using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private int _value;

        public StatType StatType => _statType;
        public int Value => _value;

        public Stat(StatType statType, int value)
        {
            _statType = statType;
            _value = value;
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}