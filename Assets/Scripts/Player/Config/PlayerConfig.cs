using System.Collections.Generic;
using GamePlay;
using Player.Enum;
using UnityEngine;

namespace Player.Config
{
    public class PlayerConfig
    {
        public int Id { get; }
        public string PlayerName { get; }
        public List<Stat> StartStats { get; }
        public SpecializationType SpecializationType { get; }
        public Dictionary<AppearanceFeature, string> AppearanceFeatures { get; }

        public PlayerConfig(string playerName, List<Stat> startStats, SpecializationType specializationType, Dictionary<AppearanceFeature, string> appearanceFeatures)
        {
            if (!PlayerPrefs.HasKey("CharacterCount"))
            {
                PlayerPrefs.SetInt("CharacterCount", 0);
            }
            int characterCount = PlayerPrefs.GetInt("CharacterCount");
            Id = characterCount;
            PlayerPrefs.SetInt("CharacterCount", Id+1);

            PlayerName = playerName;
            StartStats = startStats;
            SpecializationType = specializationType;
            AppearanceFeatures = appearanceFeatures;
        }
    }
}