using System.Collections.Generic;
using System.IO;
using ObjectPooling;
using Serialization;
using UnityEngine;

namespace PlayerCreator.Appearance
{
    public class AppearanceChanger : MonoBehaviour
    {
        private const string AppearanceFile = "PlayerAppearance.txt";
        [SerializeField] private PlayerCreator.Appearance.Appearance _appearance;
        [SerializeField] private AppearanceView _appearanceView;
        [SerializeField] private AppearanceFeaturesSpritesCollection _spritesCollection;

        private List<AppearanceElementController> _elementControllers;

        private string _savePath => Path.Combine(Application.dataPath, "Serialization/PlayerData", AppearanceFile);
        public void Start()
        {
            Debug.LogError(ObjectPool.Instance._objectPoolTransform.name);
            Dictionary<AppearanceFeature, int> features = 
                Serializator.Deserializate<Dictionary<AppearanceFeature, int>>(_savePath);

            _elementControllers = new List<AppearanceElementController>();
            foreach (var featureSprite in _spritesCollection.AppearanceFeatureSprites)
            {
                int index = 0;
                if (features != null)
                {
                    features.TryGetValue(featureSprite.AppearanceFeature, out index);
                }
                AppearanceElementView elementView = Instantiate(_appearanceView.AppearanceElementView,
                    _appearanceView.ElementGrid);
                AppearanceElementController elementController =
                    new AppearanceElementController(elementView, featureSprite,
                        _appearance.GetFeatureSprite(featureSprite.AppearanceFeature), index);
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