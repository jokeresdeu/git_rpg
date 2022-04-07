using System.Collections.Generic;
using GamePlay;
using PlayerCreator;
using UnityEngine;

namespace Player.Config
{
    public class PlayerConfig
    {
        public int Id { get; }
        public string Name { get; private set; }
        public List<Stat> Stats { get; }
        public SpecializationType SpecializationType { get; private set; }
        public List<AppearanceFeatureSprite> AppearanceFeatureSprites { get; }
        
        public PlayerConfig(string playerName, List<Stat> stats, SpecializationType specializationType, List<AppearanceFeatureSprite> appearanceFeatureSprites)
        {
            if (!PlayerPrefs.HasKey("CharacterCount"))
            {
                PlayerPrefs.SetInt("CharacterCount", 0);
            }
            int characterCount = PlayerPrefs.GetInt("CharacterCount");
            
            characterCount++;
            PlayerPrefs.SetInt("CharacterCount", characterCount);
            Id = characterCount;
            Name = playerName;
            SpecializationType = specializationType;
            AppearanceFeatureSprites = appearanceFeatureSprites;

        }
    }
}