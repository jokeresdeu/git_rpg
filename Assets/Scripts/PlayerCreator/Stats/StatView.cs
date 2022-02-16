using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private Transform _statButtonsContainer;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TMP_Text _statHeader;
        [SerializeField] private TMP_Text _statValue;

        private List<StatButton> _statsButtons;

        public event Action<StatView> OnStatViewIncreaseClicked;
        public event Action<StatView> OnStatViewDecreaseClicked;
        public event Action<StatView, int> OnStatViewValueClicked;

        public int MaxValue => _statsButtons.Count;
        public void Initialize(string statText)
        {
            _statsButtons = _statButtonsContainer.GetComponentsInChildren<StatButton>().ToList();
            _statHeader.text = statText;
            _decreaseButton.onClick.AddListener(OnDecreaseButtonClicked);
            _increaseButton.onClick.AddListener(OnIncreaseButtonClicked);
            foreach (var statButton in _statsButtons)
            {
                statButton.Initialize();
                statButton.OnClicked += OnStatButtonClicked;
            }
        }

        private void SetButtonsState(int value)
        {
            foreach (var statButton in _statsButtons)
            {
                statButton.SetState(_statsButtons.IndexOf(statButton) < value);
            }
        }

        private void OnIncreaseButtonClicked()
        {
            OnStatViewIncreaseClicked?.Invoke(this);
        }

        private void OnDecreaseButtonClicked()
        {
           OnStatViewDecreaseClicked?.Invoke(this);
        }

        private void OnStatButtonClicked(StatButton statButton)
        {
            OnStatViewValueClicked?.Invoke(this, _statsButtons.IndexOf(statButton));
        }

        public void UpdateView(bool canIncrease, bool canDecrease, int value)
        {
            _decreaseButton.enabled = canDecrease;
            _increaseButton.enabled = canIncrease;
            ChangeStat(value);
        }

        private void ChangeStat(int statValue)
        {
            _statValue.text = statValue.ToString();
            SetButtonsState(statValue);
        }
    }
}
