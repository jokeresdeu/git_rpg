using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator
{
    [CreateAssetMenu(fileName = "AppearanceSpritesCollection", menuName = "PlayerCreator/PlayerAppearance")]
    public class AppearanceFeaturesSpritesCollection : ScriptableObject
    {
        [SerializeField] private List<AppearanceFeatureSprites> _appearanceFeatureSprites;

        public List<AppearanceFeatureSprites> AppearanceFeatureSprites => _appearanceFeatureSprites;
    }
}