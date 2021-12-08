using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Serialization;
using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearanceChanger : MonoBehaviour
    {
        private const string AppearanceFile = "PlayerAppearance.txt";
        [SerializeField] private PlayerAppearance _playerAppearance;
        [SerializeField] private PlayerAppearanceView _appearanceView;
        [SerializeField] private AppearanceFeaturesSpritesCollection _spritesCollection;

        private List<PlayerAppearanceElementController> _elementControllers;

        private string _savePath => Path.Combine(Application.dataPath, "Serialization/PlayerData", AppearanceFile);
        public void Start()
        {
            Dictionary<AppearanceFeature, int> features = 
                Serializator.Deserializate<Dictionary<AppearanceFeature, int>>(_savePath);

            _elementControllers = new List<PlayerAppearanceElementController>();
            foreach (var featureSprite in _spritesCollection.AppearanceFeatureSprites)
            {
                int index = 0;
                if (features != null)
                {
                    features.TryGetValue(featureSprite.AppearanceFeature, out index);
                }
                PlayerAppearanceElementView elementView = Instantiate(_appearanceView.PlayerAppearanceElementView,
                    _appearanceView.ElementGrid);
                PlayerAppearanceElementController elementController =
                    new PlayerAppearanceElementController(elementView, featureSprite,
                        _playerAppearance.GetFeatureSprite(featureSprite.AppearanceFeature), index);
                _elementControllers.Add(elementController);
            }
        }

        private void OnSave()
        {
            Dictionary<AppearanceFeature, int> features = new Dictionary<AppearanceFeature, int>();
            foreach (var element in _elementControllers)
            {
                features.Add(element.Feature, element.Index);
            }
            Serializator.Serializate(features, _savePath);
        }
        
        
        private void OnDestroy()
        {
            OnSave();
            foreach (var element in _elementControllers)
            {
               element.Dispose();
            }
        }
    }
}