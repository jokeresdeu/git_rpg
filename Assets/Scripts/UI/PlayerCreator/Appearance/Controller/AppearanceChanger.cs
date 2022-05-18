using System.Collections.Generic;
using GamePlay.Data;
using GamePlay.Enum;
using ObjectPooling;
using Player.Enum;
using UI.Core;
using UI.PlayerCreator.Appearance.Data;
using UI.PlayerCreator.Appearance.Model;
using UI.PlayerCreator.Appearance.View;
using UnityEngine;

namespace UI.PlayerCreator.Appearance.Controller
{
    public class AppearanceChanger: IViewController
    {
        private readonly AppearanceChangerView _appearanceChangerView;
        private readonly AppearanceView _appearanceView;
        private readonly AppearanceFeaturesSpritesStorage _spritesStorage;
        public readonly AppearanceModel AppearanceModel;

        private List<AppearanceElementController> _elementControllers;

        public AppearanceChanger(AppearanceChangerView appearanceChangerView, AppearanceView appearanceView)
        {
            _appearanceChangerView = appearanceChangerView;
            _appearanceView = appearanceView;
            _spritesStorage = _appearanceChangerView.AppearanceFeaturesSpritesStorage;
            AppearanceModel = new AppearanceModel(Direction.Front);
        }

        public void Initialize(params object[] args)
        {
            _elementControllers = new List<AppearanceElementController>();
            _appearanceView.SetModel(AppearanceModel);
            foreach (var featureSprite in _spritesStorage.AppearanceFeaturesSpritesCollections)
            {
                AppearanceElementView elementView = ObjectPool.Instance.GetObject(_appearanceChangerView.AppearanceElementView);
                elementView.Transform.SetParent(_appearanceChangerView.ElementGrid);
                elementView.Transform.localScale = Vector3.one;
                AppearanceElementController elementController =
                    new AppearanceElementController(elementView, featureSprite);
                _elementControllers.Add(elementController);
                if (!AppearanceModel.AppearanceFeaturesSprites.TryGetValue(featureSprite.AppearanceFeature, out var directionalSprite))
                {
                    directionalSprite = featureSprite.DirectionalSpritesCollection[0];
                }
                AppearanceFeatureSpritesChanged(featureSprite.AppearanceFeature, directionalSprite);
                elementController.OnAppearanceFeatureSpritesChanged += AppearanceFeatureSpritesChanged;
            }
            _appearanceView.Show();
            _appearanceChangerView.Show();
        }

        public void Complete()
        {
            _appearanceView.Hide();
            _appearanceChangerView.Hide();
            foreach (var element in _elementControllers)
            {
                element.OnAppearanceFeatureSpritesChanged -= AppearanceFeatureSpritesChanged;
                element.Dispose();
            }
        }

        private void AppearanceFeatureSpritesChanged(AppearanceFeature appearanceFeature, DirectionalSpritesContainer directionalSpritesContainer)
        {
            AppearanceModel.ChangeAppearanceFeatureSprite(appearanceFeature, directionalSpritesContainer);
        }
    }
}