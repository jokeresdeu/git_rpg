using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class EquipmentRenderer
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Equipment _equipment;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public Equipment Equipment => _equipment;
    }
}