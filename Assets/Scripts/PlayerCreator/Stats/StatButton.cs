using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats
{
    public class StatButton : MonoBehaviour 
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public event Action<StatButton> OnClicked;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
        
        public void Initialize()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        public void SetState(bool active)
        {
            if(active)
                _image.color = Color.red;
            else 
                _image.color = Color.white;
        }

        private void ButtonClicked()
        {
            OnClicked?.Invoke(this);
        }
    }
}