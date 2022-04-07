using System;
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
        [SerializeField] private CreationTabButton _creationTabButton;
        [SerializeField] private Transform _creationTabsHolder;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_Text _viewHeader;
        [SerializeField] private AppearanceView _appearanceView;
        
        [Header("Specialization")]
        [SerializeField] private SpecializationView _specializationView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        
        [SerializeField] private StatsChangerView _statView;
        
        public StatsChangerView StatView => _statView;
        public SpecializationView SpecializationView => _specializationView;
        public SpecializationConfigsStorage SpecializationConfigsStorage => _specializationConfigsStorage;
        public CreationTabButton CreationTabButton => _creationTabButton;

        public Transform CreationTabsHolder => _creationTabsHolder;
        public event Action OnStartClicked;
        public event Action<string> OnNameChanged; 
        
        public void Initialize()
        {
            _startButton.onClick.AddListener(() => OnStartClicked?.Invoke());
            _nameInputField.onEndEdit.AddListener((value) => OnNameChanged?.Invoke(value));
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _nameInputField.onValueChanged.RemoveAllListeners();
            
        }
    }
}