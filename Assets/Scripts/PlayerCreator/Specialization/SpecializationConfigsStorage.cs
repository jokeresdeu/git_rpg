using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    [CreateAssetMenu(fileName = "SpecializationConfigsStorage", menuName = "PlayerCreator/SpecializationConfigs")]
    public class SpecializationConfigsStorage : ScriptableObject
    {
        [SerializeField] private SpecializationType _defaultSpecialization;
        [SerializeField] private List<SpecializationConfig> _specializationConfigs;
        
        public SpecializationType DefaultSpecialization => _defaultSpecialization;
        public List<SpecializationConfig> SpecializationConfigs => _specializationConfigs;
    }
}