using System;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerCreator.Appearance.View
{
    public class AppearanceElementView : MonoBehaviour, IPoolable
    {
        [SerializeField] private TMP_Text _elementHeader;
        [SerializeField] private TMP_Text _styleHeader;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
      
        public TMP_Text ElementHeader => _elementHeader;
        public TMP_Text StyleHeader => _styleHeader;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> OnReturnToPool;

        public event Action OnNextClicked;
        public event Action OnPrevClicked;

        public void Show()
        {
            GameObject.SetActive(true);
            _leftArrow.onClick.AddListener(()=>OnPrevClicked?.Invoke());
            _rightArrow.onClick.AddListener(()=> OnNextClicked?.Invoke());
        }

        public void Hide()
        {
            _leftArrow.onClick.RemoveAllListeners();
            _rightArrow.onClick.RemoveAllListeners();
            OnReturnToPool?.Invoke(this);
        }
    }
}