using System;
using System.Collections.Generic;
using System.Linq;
using CoreUI;
using ObjectPooling;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class SpecializationChanger : IViewController
    {
        private readonly SpecializationConfigsStorage _specializationConfigsStorage;
        private readonly SpecializationView _specializationView;

        private SpecializationModel _specializationModel; 
        private List<SkillView> _skillViews;
        private List<StatView> _statViews;
        private ObjectPool _objectPool;
        private int _currentIndex = 0;
        
        public SpecializationChanger(SpecializationView specializationView,  SpecializationConfigsStorage specializationConfigsStorage)
        {
            _specializationView = specializationView;
            _specializationConfigsStorage = specializationConfigsStorage;
            _objectPool = ObjectPool.Instance;
            _skillViews = new List<SkillView>();
            _statViews = new List<StatView>();
        }

        public void Initialize(params object[] args)
        {
            if (args == null || args.Length < 1 || !args.Any(arg => arg is SpecializationModel))
            {
                throw new NullReferenceException($"There is no args for {nameof(SpecializationChanger)}");
            }

            object model = args.First(arg => arg is SpecializationModel);
            _specializationModel = model as SpecializationModel;
            ChangeSpecialization();
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
            ClearView();
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            _specializationView.SpecializationIcon.sprite = config.SpecializationIcon;
            _specializationView.SpecializationName.text = config.SpecializationName;
            _specializationView.Description.text = config.SpecializationDescription;
            
            foreach (var stat in config.StartStats)
            {
                StatView statView = _objectPool.GetObject(_specializationView.StatView);
                statView.transform.SetParent(_specializationView.StatContainer);
                statView.transform.localScale = Vector3.one;
                statView.StatAmount.text = stat.Value.ToString();
                statView.StatType.text = stat.StatType.ToString();
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
            _specializationModel.ChangeSpecialization(config.SpecializationType, config.StartStats);
        }

        private void ClearView()
        {
            foreach (var skillView in _skillViews)
            {
                skillView.ReturnToPool();
            }
            _skillViews.Clear();

            foreach (var statView in _statViews)
            {
                statView.ReturnToPool();
            }
            _statViews.Clear();

        }
    }
}