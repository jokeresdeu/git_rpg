using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private int _value;

        [JsonIgnore]
        public StatType StatType => _statType;
      
        [JsonIgnore]
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