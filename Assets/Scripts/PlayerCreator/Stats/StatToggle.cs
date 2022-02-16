using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats
{
    public class StatToggle : MonoBehaviour, IStatClickable
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;

        public Toggle Toggle => _toggle;
        public event Action<IStatClickable> OnClicked;
        public void Initialize()
        {
            
        }

       
    }
}