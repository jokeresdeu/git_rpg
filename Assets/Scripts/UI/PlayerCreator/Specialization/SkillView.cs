using System;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerCreator.Specialization
{
    public class SkillView : MonoBehaviour, IPoolable
    {
        [SerializeField] private TMP_Text _skillName;
        [SerializeField] private TMP_Text _skillDescription;
        [SerializeField] private Image _skillImage;
        
        public TMP_Text SkillName => _skillName;
        public TMP_Text SkillDescription => _skillDescription;
        public Image SkillImage => _skillImage;
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> OnReturnToPool;
        public void Reset()
        {
            OnReturnToPool?.Invoke(this);
        }
    }
}