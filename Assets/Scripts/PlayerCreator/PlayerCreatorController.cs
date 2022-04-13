﻿using System.Collections.Generic;
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
        private StatsModel _statsModel;

        private SpecializationChanger _specializationChanger;
        private SpecializationModel _specializationModel;

        private IViewController _currentController;

        private Dictionary<CreationTabSwitcher, СreationTab> _availableTabsSwitchers;
        private CreationTabSwitcher _selectedSwitcher;

        private string _playerName;

        private List<Stat> _stats;

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
            _stats = new List<Stat>();
            _statsModel = new StatsModel(_stats, 10);
            _specializationModel = new SpecializationModel(_stats);

            _statChanger = new StatChanger(_creatorView.StatView);
            _specializationChanger = new SpecializationChanger(_creatorView.SpecializationView,
                _creatorView.SpecializationConfigsStorage);

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
                    _specializationChanger.Initialize(_specializationModel);
                    return _specializationChanger;
                case СreationTab.Stats:
                    _statChanger.Initialize(_statsModel);
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

            PlayerConfig playerConfig =
                new PlayerConfig(_playerName, _stats, _specializationModel.SpecializationType, new List<AppearanceFeatureSprite>());
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