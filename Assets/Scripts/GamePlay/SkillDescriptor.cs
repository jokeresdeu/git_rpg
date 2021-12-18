using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class SkillDescriptor
    {
        [SerializeField] private SkillType _skillType;
        [SerializeField] private string _skillName;
        [SerializeField] private string _skillDescription;
        [SerializeField] private Sprite _skillSprite;

        public SkillType SkillType => _skillType;
        public string SkillName => _skillName;
        public string SkillDescription => _skillDescription;
        public Sprite SkillSprite => _skillSprite;
    }
}