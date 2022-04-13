using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace Player.Config
{
    public class PlayerConfig
    {
        public int Id { get; }
        public string PlayerName { get; }
        public List<Stat> StartStats { get; }
        public SpecializationType SpecializationType { get; }
        public List<AppearanceFeatureSprite> AppearanceFeatureSprites { get; }
        
        public PlayerConfig(string playerName, List<Stat> startStats, SpecializationType specializationType, List<AppearanceFeatureSprite> featureSprites)
        {
            if (!PlayerPrefs.HasKey("CharacterCount"))
            {
                PlayerPrefs.SetInt("CharacterCount", 0);
            }
            int characterCount = PlayerPrefs.GetInt("CharacterCount");
            Id = characterCount;
            PlayerPrefs.SetInt("CharacterCount", Id+1);

            PlayerName = playerName;
            this.StartStats = startStats;
            SpecializationType = specializationType;
            AppearanceFeatureSprites = featureSprites;
        }
    }
}