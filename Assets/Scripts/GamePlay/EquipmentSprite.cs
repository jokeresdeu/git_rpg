using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class EquipmentSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Equipment _equipment;

        public Sprite Sprite => _sprite;

        public Equipment Equipment => _equipment;
    }
}