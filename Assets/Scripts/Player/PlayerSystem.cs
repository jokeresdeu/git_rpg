using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField] private List<PlayerDirection> _playerDirections;

        public List<PlayerDirection> PlayerDirections => _playerDirections;
    }

    [Serializable]
    public class PlayerDirection
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private PlayerGraphic _playerGraphic;

        public Direction Direction => _direction;
        public PlayerGraphic PlayerGraphic => _playerGraphic;
    }

    public enum Direction
    {
        None = 0,
        Forward = 1,
        Back = 2,
        Left = 3,
        Right = 4,
    }
}
