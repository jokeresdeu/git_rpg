using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator
{
    public class CreationTabButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private СreationTab _creationTab;

        public event Action<СreationTab> OnButtonClicked;

        public void Initialize()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnButtonClicked?.Invoke(_creationTab);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
    }
}