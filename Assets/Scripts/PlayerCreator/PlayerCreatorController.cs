using CoreUI;
using Player.Config;
using PlayerCreator.Specialization;
using PlayerCreator.Stats;
using Serialization;
using UnityEngine;

namespace PlayerCreator
{
    public class PlayerCreatorController : MonoBehaviour //3 - PlayerCreationController + PlayerConfig
                                                         //4 - PlayerCreationController + SpecializationChanger
    //5 - PlayerCreationController + StatsChanger(SpecializationChanger)
    //6 - PlayerCreationController + Serialization
    //7 - PlayerCreationController + AppearanceChanger
    {
        [SerializeField] private PlayerCreatorView _creatorView;

        private StatChanger _statChanger;
        private StatsModel _statsModel;
        
        private SpecializationChanger _specializationChanger;
        private SpecializationModel _specializationModel;
        
        private PlayerConfig _playerConfig;

        private IViewController _currentController;
        
        private void Start()
        {
            _playerConfig = new PlayerConfig();
            
            _statsModel = new StatsModel(_playerConfig.Stats, 10);
            _specializationModel = new SpecializationModel(_playerConfig.Stats);
            
            _statChanger = new StatChanger(_creatorView.StatView);
            _specializationChanger = new SpecializationChanger(_creatorView.SpecializationView,
                _creatorView.SpecializationConfigsStorage);

            foreach (var button in _creatorView.CreationTabButtons)
            {
                button.Initialize();
                button.OnButtonClicked += OnTabChanged;
            }
            
            _currentController = GetAndInitializeController(СreationTab.Specialization);
        }

        private void OnTabChanged(СreationTab creationTab)
        {
            _currentController?.Complete();
            _currentController = GetAndInitializeController(creationTab);
        }

        private IViewController GetAndInitializeController(СreationTab creationTab)
        {
            switch (creationTab)
            {
                case СreationTab.Specialization:
                    _specializationChanger.Initialize(_specializationModel);
                    return _specializationChanger;
                case СreationTab.Stats:
                    _statChanger.Initialize(_statsModel);
                    return _statChanger;
                default:
                    return null;
            }
        }

        private void OnDestroy()
        {
            foreach (var button in _creatorView.CreationTabButtons)
            {
                button.OnButtonClicked -= OnTabChanged;
            }
            _playerConfig.SetSpecialization(_specializationModel.SpecializationType);
            Debug.LogError(_playerConfig.SpecializationType);
            foreach (var stat in _playerConfig.Stats)
            {
                Debug.LogError($"{stat.StatType} = {stat.Value}");
            }
        }

        private void OnStartGameClicked()
        {
            Serializator.Serializate(_playerConfig, "PlayerConfig");
        }
    }
}