using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor.Common;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.ExampleScripts
{
    /// <summary>
    /// An example of how to change character's equipment.
    /// </summary>
    public class CharacterEquipment : MonoBehaviour
    {
        public Character4D Character;

        public void EquipRandomArmor()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Armor.Count);
            var randomItem = Character.SpriteCollection.Armor[randomIndex];

            Character.EquipArmor(randomItem);
        }

        public void RemoveArmor()
        {
            Character.EquipArmor(null as SpriteGroupEntry);
        }

        public void EquipRandomHelmet()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Armor.Count);
            var randomItem = Character.SpriteCollection.Armor[randomIndex];

            Character.EquipHelmet(randomItem);
        }

        public void RemoveHelmet()
        {
            Character.EquipHelmet(null as SpriteGroupEntry);
        }

        public void EquipRandomShield()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Shield.Count);
            var randomItem = Character.SpriteCollection.Shield[randomIndex];

            Character.EquipShield(randomItem);
        }

        public void RemoveShield()
        {
            Character.EquipShield(null as SpriteGroupEntry);
        }

        public void EquipRandomWeapon()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.MeleeWeapon1H.Count);
            var randomItem = Character.SpriteCollection.MeleeWeapon1H[randomIndex];

            Character.EquipMeleeWeapon1H(randomItem);
        }

        public void RemoveWeapon()
        {
            Character.EquipMeleeWeapon1H(null as SpriteGroupEntry);
        }
    }
}