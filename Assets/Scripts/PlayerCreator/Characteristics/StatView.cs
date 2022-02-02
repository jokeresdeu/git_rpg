using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Characteristics
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private Transform _statButtonsContainer;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TMP_Text _statHeader;
        [SerializeField] private TMP_Text _statValue;

        private List<StatButton> _characteristicButtons;
        private StatType _statType;

        public event Action<StatType> OnStatViewValueDecreased;
        public event Action<StatType> OnStatViewValueIncreased;
        public event Action<StatType, int> OnStatViewValueChanged;

        public int MaxValue => _characteristicButtons.Count;
        
        public void Initialize(StatType statType)
        {
            _characteristicButtons = _statButtonsContainer.GetComponentsInChildren<StatButton>().ToList();

            foreach (var button in _characteristicButtons)
            {
                button.Initialize();
                button.OnClicked += CharacteristicButtonClicked;
            }

            _decreaseButton.onClick.AddListener(CharacteristicDecreased);
            _increaseButton.onClick.AddListener(CharacteristicIncreased);
            _statType = statType;
            _statHeader.text = _statType.ToString();
        }

        private void CharacteristicButtonClicked(StatButton button)
        {
            OnStatViewValueChanged?.Invoke(_statType, _characteristicButtons.IndexOf(button) + 1);
        }

        private void CharacteristicIncreased()
        {
            OnStatViewValueIncreased?.Invoke(_statType);
        }

        private void CharacteristicDecreased()
        {
            OnStatViewValueDecreased?.Invoke(_statType);
        }

        public void Update(bool canDecrease, bool canIncrease, int value)
        {
            _increaseButton.enabled = canIncrease;
            _decreaseButton.enabled = canDecrease;
            _statValue.text = value.ToString();
        }
    }
}
