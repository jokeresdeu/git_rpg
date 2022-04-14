using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private StatType _type;
        [SerializeField] private int _value;

        public StatType Type => _type;
        public int Value => _value;

        public Stat(StatType type, int value)
        {
            _type = type;
            _value = value;
        }
        
        public void SetValue(int value)
        {
            _value = value;
        }

        public Stat Clone()
        {
            return new Stat(_type, _value);
        }
    }
}