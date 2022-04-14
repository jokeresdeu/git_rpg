﻿using System;
using System.Collections.Generic;
using GamePlay;
using Player;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    [Serializable]
    public class SpecializationConfig
    {
        [SerializeField] private SpecializationType _specializationType;
        [SerializeField] private string _specializationName;
        [SerializeField] private Sprite _specializationIcon;
        [SerializeField] private List<EquipmentSprite> _equipmentSprites;
        [TextArea]
        [SerializeField] private string _specializationDescription;
        [SerializeField] private List<Stat> _startStats;
        [SerializeField] private List<SkillDescriptor> _startSkills;
        
       public SpecializationType SpecializationType => _specializationType;
       public string SpecializationName => _specializationName;
       public Sprite SpecializationIcon => _specializationIcon;
       public string SpecializationDescription => _specializationDescription;
       public List<Stat> StartStats => _startStats;
       public List<SkillDescriptor> StartSkills => _startSkills;
       public List<EquipmentSprite> EquipmentSprites => _equipmentSprites;

    }
}