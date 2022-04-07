using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator
{
    public class CreationTabButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        
        public event Action<CreationTabButton> OnButtonClicked;

        public void Initialize(string text)
        {
            _buttonText.text = text;
            _button.onClick.AddListener(ButtonClicked);
        }

        public void ChangeState(bool selected)
        {
            _button.interactable = !selected;
        }
        

        private void ButtonClicked()
        {
            OnButtonClicked?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
    }
}