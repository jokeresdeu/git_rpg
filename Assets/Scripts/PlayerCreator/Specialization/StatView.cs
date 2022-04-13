using System;
using ObjectPooling;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class StatView : MonoBehaviour, IPoolable
    {
        [SerializeField] private TMP_Text _statType;
        [SerializeField] private TMP_Text _statAmount;
        
        public TMP_Text StatType => _statType;
        public TMP_Text StatAmount => _statAmount;
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> OnReturnToPool;
        public void Reset()
        {
            OnReturnToPool?.Invoke(this);
        }
    }
}