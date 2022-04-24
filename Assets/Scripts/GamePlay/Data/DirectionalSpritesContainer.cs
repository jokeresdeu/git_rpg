using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Data
{
    [CreateAssetMenu(fileName = "DirectionalSpritesContainer", menuName =  "Appearance/DirectionalSpritesContainer")]
    public class DirectionalSpritesContainer : ScriptableObject
    {
        [SerializeField] private List<DirectionalSprite> _directionSprites;
        public List<DirectionalSprite> DirectionSprites => _directionSprites;
    }
}