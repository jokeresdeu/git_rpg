using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Characteristics
{
    public class StatButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public Image Image => _image;
        public Button Button => _button;

        public event Action<StatButton> OnClicked;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
        
        public void Initialize()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnClicked?.Invoke(this);
        }
    }
}