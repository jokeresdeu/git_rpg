using System.Collections.Generic;
using CoreUI;
using ObjectPooling;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class SpecializationChanger : IViewController
    {
        private readonly SpecializationConfigsStorage _specializationConfigsStorage;
        private readonly SpecializationAppearance _specializationAppearance;
        private readonly SpecializationView _specializationView;
        private readonly List<SkillView> _skillViews;
        private readonly List<StatView> _statViews;
        private readonly ObjectPool _objectPool;

        public SpecializationModel SpecializationModel { get; private set; }
        
        private int _currentIndex;
        
        public SpecializationChanger(SpecializationView specializationView,  SpecializationConfigsStorage specializationConfigsStorage, SpecializationAppearance specializationAppearance)
        {
            _specializationView = specializationView;
            _specializationConfigsStorage = specializationConfigsStorage;
            _specializationAppearance = specializationAppearance;
            _objectPool = ObjectPool.Instance;
            _skillViews = new List<SkillView>();
            _statViews = new List<StatView>();
            
            SpecializationConfig defaultConfig = _specializationConfigsStorage.SpecializationConfigs.Find(config =>
                config.SpecializationType == _specializationConfigsStorage.DefaultSpecialization) ?? _specializationConfigsStorage.SpecializationConfigs[0];
            _currentIndex = _specializationConfigsStorage.SpecializationConfigs.IndexOf(defaultConfig);
            SpecializationModel = new SpecializationModel(defaultConfig.SpecializationType, defaultConfig.StartStats);
            _specializationAppearance.SetEquipment(defaultConfig.EquipmentSprites);
        }

        public void Initialize(params object[] args)
        {
            UpdateView();
            _specializationView.LeftArrow.onClick.AddListener(PreviousSpecialization);
            _specializationView.RightArrow.onClick.AddListener(NextSpecialization);
            _specializationView.Show();
        }

        public void Complete()
        {
            ClearView();
            _specializationView.LeftArrow.onClick.RemoveListener(PreviousSpecialization);
            _specializationView.RightArrow.onClick.RemoveListener(NextSpecialization);
            _specializationView.Hide();
        }
        
        private void NextSpecialization()
        {
            _currentIndex++;
            if (_currentIndex > _specializationConfigsStorage.SpecializationConfigs.Count - 1)
            {
                _currentIndex = 0;
            }
            ChangeSpecialization();
            UpdateView();
        }

        private void PreviousSpecialization()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _specializationConfigsStorage.SpecializationConfigs.Count - 1;
            }
            ChangeSpecialization();
            UpdateView();
        }

        private void UpdateView()
        {
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            _specializationView.SpecializationIcon.sprite = config.SpecializationIcon;
            _specializationView.SpecializationName.text = config.SpecializationName;
            _specializationView.Description.text = config.SpecializationDescription;
            foreach (var stat in config.StartStats)
            {
                StatView statView = _objectPool.GetObject(_specializationView.StatView);
                statView.transform.SetParent(_specializationView.StatContainer); //GetComponent<Transform>
                statView.transform.localScale = Vector3.one;
                statView.StatAmount.text = stat.Value.ToString();
                statView.StatType.text = stat.Type.ToString();
                _statViews.Add(statView);
            }
            
            foreach (var skill in config.StartSkills)
            {
                SkillView skillView = _objectPool.GetObject(_specializationView.SkillView);
                skillView.transform.SetParent(_specializationView.SkillContainer);
                skillView.transform.localScale = Vector3.one;
                skillView.SkillDescription.text = skill.SkillDescription;
                skillView.SkillName.text = skill.SkillName;
                skillView.SkillImage.sprite = skill.SkillSprite;
                _skillViews.Add(skillView);
            }
        }

        private void ChangeSpecialization()
        {
            ClearView();
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            SpecializationModel = new SpecializationModel(config.SpecializationType, config.StartStats);
            _specializationAppearance.SetEquipment(config.EquipmentSprites);
        }

        private void ClearView()
        {
            foreach (var skillView in _skillViews)
            {
                skillView.Reset();
            }
            _skillViews.Clear();

            foreach (var statView in _statViews)
            {
                statView.Reset();
            }
            _statViews.Clear();

        }
    }
}