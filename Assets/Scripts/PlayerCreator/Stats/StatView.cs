using System;
using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats
{
    public class StatView : MonoBehaviour, IPoolable 
    {
        [SerializeField] private Transform _statButtonsContainer;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TMP_Text _statHeader;
        [SerializeField] private TMP_Text _statValue;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        
        public List<StatButton> StatsButtons { get; private set; }
        public event Action OnDecreased;
        public event Action OnIncreased;
        public event Action<int> OnStatValueChanged;

        public event Action<IPoolable> OnReturnToPool;
        public void ReturnToPool()
        {
            throw new NotImplementedException();
        }
        
        private void Awake()
        {
            StatsButtons = _statButtonsContainer.GetComponentsInChildren<StatButton>().ToList();
            _decreaseButton.onClick.AddListener(Decreased);
            _increaseButton.onClick.AddListener(Increased);
            foreach (var statButton in StatsButtons)
            {
                statButton.Initialize();
                statButton.OnClicked += StatValueChanged;
            }
        }

        protected void OnDestroy()
        {
            _decreaseButton.onClick.RemoveListener(Decreased);
            _increaseButton.onClick.RemoveListener(Increased);
            foreach (var statButton in StatsButtons)
            {
                statButton.OnClicked -= StatValueChanged;
            }
        }

        public void SetHeader(string value)
        {
            _statHeader.text = value;
        }

        public void SetValue(int value)
        {
            _statValue.text = value.ToString();
        }

        public void SetDecreaseStatus(bool status)
        {
            _decreaseButton.enabled = status;
        }
        
        private void Increased()
        {
            OnIncreased?.Invoke();
        }

        private void Decreased()
        {
           OnDecreased?.Invoke();
        }

        private void StatValueChanged(StatButton statButton)
        {
            OnStatValueChanged?.Invoke(StatsButtons.IndexOf(statButton));
        }
    }
}
