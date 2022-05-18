using System;
using System.Collections.Generic;
using GamePlay.Data;
using GamePlay.Enum;
using Player.Enum;
using UnityEngine;

namespace UI.PlayerCreator.Appearance.Model
{
    public class AppearanceModel
    {
        private readonly Direction _currentDirection;
        public readonly Dictionary<AppearanceFeature, DirectionalSpritesContainer> AppearanceFeaturesSprites;
        
        public event Action<AppearanceFeature, Sprite> OnAppearanceFeatureSpriteChanged;

        public AppearanceModel(Direction currentDirection)
        {
            _currentDirection = currentDirection;
            AppearanceFeaturesSprites = new Dictionary<AppearanceFeature, DirectionalSpritesContainer>();
        }

        public void ChangeAppearanceFeatureSprite(AppearanceFeature appearanceFeature, DirectionalSpritesContainer directionalSprites)
        {
            AppearanceFeaturesSprites[appearanceFeature] = directionalSprites;
            OnAppearanceFeatureSpriteChanged?.Invoke(appearanceFeature, directionalSprites.DirectionSprites.Find(sprite => sprite.Direction == _currentDirection).Sprite);
        }
    }
}