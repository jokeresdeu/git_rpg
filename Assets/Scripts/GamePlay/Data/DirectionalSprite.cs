using System;
using GamePlay.Enum;
using UnityEngine;

namespace GamePlay.Data
{
    [Serializable]
    public class DirectionalSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Direction _direction;

        public Sprite Sprite => _sprite;
        public Direction Direction => _direction;
    }
}