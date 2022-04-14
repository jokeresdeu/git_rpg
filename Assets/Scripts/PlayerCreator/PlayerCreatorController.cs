using System.Collections.Generic;
using System.IO;
using CoreUI;
using GamePlay;
using Player;
using Player.Config;
using PlayerCreator.Specialization;
using PlayerCreator.Stats;
using Serialization;
using UnityEngine;

namespace PlayerCreator
{
    public class PlayerCreatorController : MonoBehaviour
    {
        [SerializeField] private PlayerCreatorView _creatorView;

        private StatChanger _statChanger;
        private SpecializationChanger _specializationChanger;

        private IViewController _currentController;

        private Dictionary<CreationTabSwitcher, СreationTab> _availableTabsSwitchers;
        private CreationTabSwitcher _selectedSwitcher;

        private string _playerName;

        private SpecializationType _currentSpecialization;

        private void Start()
        {
            List<СreationTab> availableTabs = new List<СreationTab> {СreationTab.Specialization, СreationTab.Stats};
            СreationTab defaultTab = СreationTab.Specialization;
            _creatorView.Initialize();
            _availableTabsSwitchers = new Dictionary<CreationTabSwitcher, СreationTab>();
            foreach (var tab in availableTabs)
            {
                CreationTabSwitcher tabSwitcher = Instantiate(_creatorView.CreationTabSwitcher, _creatorView.SwitchersContainer);
                tabSwitcher.Initialize(tab.ToString());
                tabSwitcher.OnSwitcherSelected += SwitcherSelected;
                _availableTabsSwitchers.Add(tabSwitcher, tab);
                if (tab != defaultTab)
                {
                    continue;
                }

                _selectedSwitcher = tabSwitcher;
                _selectedSwitcher.SetSelectionState(true);
            }

            _creatorView.OnNameChanged += NameChanged;
            _creatorView.OnSaveClicked += SaveClicked;

            _specializationChanger = new SpecializationChanger(_creatorView.SpecializationView,
                _creatorView.SpecializationConfigsStorage, _creatorView.SpecializationAppearance);
            _statChanger = new StatChanger(_creatorView.StatView);
            
            _currentController = GetAndInitializeController(СreationTab.Specialization);
        }

        private void SwitcherSelected(CreationTabSwitcher creationTabSwitcher)
        {
            _selectedSwitcher.SetSelectionState(false);
            _selectedSwitcher = creationTabSwitcher;
            _selectedSwitcher.SetSelectionState(true);
            ChangeTab(_availableTabsSwitchers[creationTabSwitcher]);
        }

        private void ChangeTab(СreationTab creationTab)
        {
            _currentController?.Complete();
            _currentController = GetAndInitializeController(creationTab);
        }

        private IViewController GetAndInitializeController(СreationTab creationTab)
        {
            switch (creationTab)
            {
                case СreationTab.Specialization:
                    _specializationChanger.Initialize();
                    return _specializationChanger;
                case СreationTab.Stats:
                    StatsModel statsModel = _currentSpecialization == SpecializationType.None || _currentSpecialization != _specializationChanger.SpecializationModel.SpecializationType 
                        ? new StatsModel(_specializationChanger.SpecializationModel.Stats, _specializationChanger.SpecializationModel.FreeStats) : _statChanger.StatsModel;
                    _currentSpecialization = _specializationChanger.SpecializationModel.SpecializationType;
                    _statChanger.Initialize(statsModel);
                    return _statChanger;
                default:
                    return null;
            }
        }

        private void NameChanged(string playerName)
        {
            _playerName = playerName;
        }

        private void SaveClicked()
        {
            if (string.IsNullOrEmpty(_playerName))
            {
                Debug.Log("Can`t create player without name");
                return;
            }

            List<Stat> playerStats;
            if (_statChanger.StatsModel == null || _statChanger.StatsModel.FreeStats > 0)
            {
                playerStats = _specializationChanger.SpecializationModel.DefaultStats;
                Debug.Log("You have free stats, do you want to use default specialization stats");
            }
            else
            {
                playerStats = _statChanger.StatsModel.Stats;
            }

            PlayerConfig playerConfig =
                new PlayerConfig(_playerName, playerStats, _specializationChanger.SpecializationModel.SpecializationType,
                    new List<AppearanceFeatureSprite>());
            Serializator.Serializate(playerConfig, Path.Combine(Application.dataPath, "Serialization/PlayerData", $"Player_{playerConfig.Id}.json"));
        }


        private void OnDestroy()
        {
            foreach (var switchers in _availableTabsSwitchers.Keys)
            {
                switchers.OnSwitcherSelected -= SwitcherSelected;
            }

            _creatorView.OnNameChanged -= NameChanged;
            _creatorView.OnSaveClicked -= SaveClicked;
        }
    }
}