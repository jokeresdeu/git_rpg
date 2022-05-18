using UI.Core;
using UI.PlayerCreator.Appearance.Data;
using UnityEngine;

namespace UI.PlayerCreator.Appearance.View
{
    public class AppearanceChangerView : BaseView
    {
        [SerializeField] private AppearanceFeaturesSpritesStorage _appearanceFeaturesSpritesStorage;
        [SerializeField] private AppearanceElementView _appearanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public AppearanceFeaturesSpritesStorage AppearanceFeaturesSpritesStorage => _appearanceFeaturesSpritesStorage;
        public AppearanceElementView AppearanceElementView => _appearanceElementView;
        public Transform ElementGrid => _elementsGrid;
    }
}