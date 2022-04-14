using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class SpecializationAppearance : MonoBehaviour
    {
        [SerializeField] private List<EquipmentRenderer> _equipment;

        public void SetEquipment(List<EquipmentSprite> sprites)
        {
            foreach (var equipment in _equipment)
            {
                EquipmentSprite equipmentSprite = sprites.Find(sp => sp.Equipment == equipment.Equipment);
                equipment.SpriteRenderer.sprite = equipmentSprite?.Sprite;
                equipment.SpriteRenderer.enabled = true;
            }
        }

        public void SetEquipmentVisibility(bool visible)
        {
            foreach (var equipmentRenderer in _equipment)
            {
                equipmentRenderer.SpriteRenderer.enabled = visible;
            }
        }
    }
}