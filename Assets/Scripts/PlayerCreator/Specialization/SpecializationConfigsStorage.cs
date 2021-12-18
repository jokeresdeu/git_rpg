using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    [CreateAssetMenu(fileName = "SpecializationConfigsStorage", menuName = "PlayerCreator/SpecializationConfigs")]
    public class SpecializationConfigsStorage : ScriptableObject
    {
        [SerializeField] private List<SpecializationConfig> _specializationConfigs;
        
        public List<SpecializationConfig> SpecializationConfigs => _specializationConfigs;
    }
}