using System;
using System.Collections.Generic;
using Player.Enum;
using PlayerCreator.Appearance.Model;
using UnityEngine;

namespace PlayerCreator.Appearance.View
{
    public class AppearanceView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _helmet;
        [SerializeField] private SpriteRenderer _hair;
        [SerializeField] private List<AppearanceFeatureSpriteRenderer> _spriteRenderers;
        
        private AppearanceModel _appearanceModel;

        public void Show()
        {
            _helmet.enabled = false;
            _hair.enabled = true;
        }

        public void SetModel(AppearanceModel appearanceModel)
        {
            if (_appearanceModel != null)
            {
                _appearanceModel.OnAppearanceFeatureSpriteChanged -= AppearanceFeatureSpriteChanged;
            }
            
            _appearanceModel = appearanceModel;

            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.SpriteRenderer.sprite = null;
            }

            _appearanceModel.OnAppearanceFeatureSpriteChanged += AppearanceFeatureSpriteChanged;
        }
        
        public void Hide()
        {
            _helmet.enabled = true;
            _hair.enabled = false;
            _appearanceModel.OnAppearanceFeatureSpriteChanged -= AppearanceFeatureSpriteChanged;
        }

        private void AppearanceFeatureSpriteChanged(AppearanceFeature appearanceFeature, Sprite sprite)
        {
            var spriteRenderer = _spriteRenderers.Find(render => render.AppearanceFeature == appearanceFeature).SpriteRenderer;
            spriteRenderer.sprite = sprite;
        }
        
        [Serializable]
        private class AppearanceFeatureSpriteRenderer
        {
            [SerializeField] private AppearanceFeature _appearanceFeature;
            [SerializeField] private SpriteRenderer _spriteRenderer;

            public AppearanceFeature AppearanceFeature => _appearanceFeature;

            public SpriteRenderer SpriteRenderer => _spriteRenderer;
        }
    }
}