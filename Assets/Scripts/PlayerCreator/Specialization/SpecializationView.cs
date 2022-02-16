using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Specialization
{
    public class SpecializationView : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private Image _specializationIcon;
        [SerializeField] private TMP_Text _specializationName;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;

        [Header("Body")] 
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _statContainer;
        [SerializeField] private StatView _statView;
        [SerializeField] private Transform _skillContainer;
        [SerializeField] private SkillView _skillView;
        
        public Image SpecializationIcon => _specializationIcon;
        public TMP_Text SpecializationName => _specializationName;
        public Button LeftArrow => _leftArrow;
        public Button RightArrow => _rightArrow;
        public TMP_Text Description => _description;
        public Transform StatContainer => _statContainer;
        public StatView StatView => _statView;
        public Transform SkillContainer => _skillContainer;
        public SkillView SkillView => _skillView;
    }
}
