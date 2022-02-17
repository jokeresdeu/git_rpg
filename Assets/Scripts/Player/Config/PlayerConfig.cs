using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace Player.Config
{
    public class PlayerConfig
    {
        public int Id { get; }
        public string Name { get; }
        public List<Stat> Stats { get; }
        public SpecializationType SpecializationType { get; private set; }
        public List<AppearanceFeatureSprite> AppearanceFeatureSprites { get; }
        
        public PlayerConfig()
        {
            if (!PlayerPrefs.HasKey("CharacterCount"))
            {
                PlayerPrefs.SetInt("CharacterCount", 0);
            }
            int characterCount = PlayerPrefs.GetInt("CharacterCount");
            
            characterCount++;
            Id = characterCount;
            Stats = new List<Stat>();
            AppearanceFeatureSprites = new List<AppearanceFeatureSprite>();
        }

        public void SetSpecialization(SpecializationType specializationType)
        {
            SpecializationType = specializationType;
        }
    }
}