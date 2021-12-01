using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearanceChanger : MonoBehaviour
    {
        [SerializeField] private PlayerAppearance _playerAppearance;
        [SerializeField] private PlayerAppearanceView _appearanceView;
        [SerializeField] private List<AppearanceFeatureSprites> _appearanceFeatureSprites;

        private List<PlayerAppearanceElementController> _elementControllers;

        public void Start()
        {
            _elementControllers = new List<PlayerAppearanceElementController>();
            foreach (var featureSprite in _appearanceFeatureSprites)
            {
                PlayerAppearanceElementView elementView = Instantiate(_appearanceView.PlayerAppearanceElementView,
                    _appearanceView.ElementGrid);
                PlayerAppearanceElementController elementController =
                    new PlayerAppearanceElementController(elementView, featureSprite,
                        _playerAppearance.GetFeatureSprite(featureSprite.AppearanceFeature));
                _elementControllers.Add(elementController);
            }
        }

        private void OnDestroy()
        {
            foreach (var element in _elementControllers)
            {
               element.Dispose();
            }
        }
    }
}