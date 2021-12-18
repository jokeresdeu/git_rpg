using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class SpecializationChanger : MonoBehaviour
    {
        [SerializeField] private PlayerSpecializationView _specializationView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;

        private List<GameObject> _skillViews;
        private List<GameObject> _statViews;
        
        private int _currentIndex = 0;
        
        private void Start()
        {
            _skillViews = new List<GameObject>();
            _statViews = new List<GameObject>();
            ChangeSpecialization();
            _specializationView.LeftArrow.onClick.AddListener(PreviousSpecialization);
            _specializationView.RightArrow.onClick.AddListener(NextSpecialization);
        }

        private void NextSpecialization()
        {
            _currentIndex++;
            if (_currentIndex > _specializationConfigsStorage.SpecializationConfigs.Count - 1)
            {
                _currentIndex = 0;
            }
            ChangeSpecialization();
        }

        private void PreviousSpecialization()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _specializationConfigsStorage.SpecializationConfigs.Count - 1;
            }
            ChangeSpecialization();
        }

        private void ChangeSpecialization()
        {
            foreach (var skillView in _skillViews)
            {
                Destroy(skillView);
            }
            _skillViews.Clear();

            foreach (var statView in _statViews)
            {
                Destroy(statView);
            }
            _skillViews.Clear();
            
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            _specializationView.SpecializationIcon.sprite = config.SpecializationIcon;
            _specializationView.SpecializationName.text = config.SpecializationName;
            _specializationView.Description.text = config.SpecializationDescription;

            foreach (var stat in config.StartStats)
            {
                StatView statView = Instantiate(_specializationView.StatView, _specializationView.StatContainer);
                statView.StatAmount.text = stat.Amount.ToString();
                statView.StatType.text = stat.StatType.ToString();
                _statViews.Add(statView.gameObject);
            }

            foreach (var skill in config.StartSkills)
            {
                SkillView skillView = Instantiate(_specializationView.SkillView, _specializationView.SkillContainer);
                skillView.SkillDescription.text = skill.SkillDescription;
                skillView.SkillName.text = skill.SkillName;
                skillView.SkillImage.sprite = skill.SkillSprite;
                _skillViews.Add(skillView.gameObject);
            }
        }

    }
}