using System;
using System.Collections.Generic;
using PlayerCreator.Appearance;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class AppearanceFeatureSprite
    {
        [SerializeField] private AppearanceFeature _appearanceFeature;
        [SerializeField] private int _spriteIndex;

        public AppearanceFeature AppearanceFeature => _appearanceFeature;
        public int SpriteIndex => _spriteIndex;
    }
}