using System;
using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hair;
        [SerializeField] private SpriteRenderer _beard;

        public SpriteRenderer Hair => _hair;
        public SpriteRenderer Beard => _beard;

        public SpriteRenderer GetFeatureSprite(AppearanceFeature feature)
        {
            switch (feature)
            {
                case AppearanceFeature.Beard: 
                    return _beard;
                case AppearanceFeature.Hair: 
                    return _hair;
                default:
                    throw new NullReferenceException($"There is no spriteRenderer for feature {feature.ToString()} ");
            }
        }
    }
}