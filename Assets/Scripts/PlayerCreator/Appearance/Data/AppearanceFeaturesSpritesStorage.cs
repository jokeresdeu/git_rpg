using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator.Appearance.Data
{
    [CreateAssetMenu(fileName = "AppearanceFeaturesSpritesStorage", menuName = "PlayerCreator/AppearanceFeaturesSpritesStorage")]
    public class AppearanceFeaturesSpritesStorage : ScriptableObject
    {
        [SerializeField] private List<AppearanceFeatureSprites> _appearanceFeaturesSpritesCollections;
        public List<AppearanceFeatureSprites> AppearanceFeaturesSpritesCollections => _appearanceFeaturesSpritesCollections;
    }
}