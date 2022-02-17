using System.Collections.Generic;
using PlayerCreator.Appearance;
using PlayerCreator.Specialization;
using PlayerCreator.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator
{
    public class PlayerCreatorView : MonoBehaviour //2 - Player creation View
    {
        [SerializeField] private List<CreationTabButton> _creationTabButtons;
        [SerializeField] private Button _saveButton;
        [SerializeField] private TMP_InputField _nameInputField;
        
        [SerializeField] private AppearanceView _appearanceView;
        
        [Header("Specialization")]
        [SerializeField] private SpecializationView _specializationView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        
        [SerializeField] private StatsChangerView _statView;
        
        public List<CreationTabButton> CreationTabButtons => _creationTabButtons;
        public StatsChangerView StatView => _statView;
        public SpecializationView SpecializationView => _specializationView;
        public SpecializationConfigsStorage SpecializationConfigsStorage => _specializationConfigsStorage;
       
    }
}