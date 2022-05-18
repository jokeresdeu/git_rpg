using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerCreator
{
    public class CreationTabSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public event Action<CreationTabSwitcher> OnSwitcherSelected;

        public void Initialize(string text)
        {
            _button.onClick.AddListener(ButtonClicked);
            _text.text = text;
        }

        public void SetSelectionState(bool selected)
        {
            _button.interactable = !selected;
        }

        private void ButtonClicked()
        {
            OnSwitcherSelected?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
    }
}