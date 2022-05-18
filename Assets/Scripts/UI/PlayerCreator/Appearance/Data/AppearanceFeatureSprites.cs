using System;
using System.Collections.Generic;
using GamePlay.Data;
using Player.Enum;
using UnityEngine;

namespace UI.PlayerCreator.Appearance.Data
{
    [Serializable]
    public class AppearanceFeatureSprites
    {
        [SerializeField] private AppearanceFeature _appearanceFeature;
        [SerializeField] private List<DirectionalSpritesContainer> _directionalSpritesCollection;
       
        public AppearanceFeature AppearanceFeature => _appearanceFeature;

        public List<DirectionalSpritesContainer> DirectionalSpritesCollection => _directionalSpritesCollection;
    }
}