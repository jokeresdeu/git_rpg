using System.Collections.Generic;
using System.IO;
using CoreUI;
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
        private CreationTabButton _currentTabButton;

        private StatChanger _statChanger;

        private SpecializationChanger _specializationChanger;

        private IViewController _currentController;
        private СreationTab _defaultTab = СreationTab.Specialization;
        private Dictionary<CreationTabButton, СreationTab> _creationTabsButtons;

        private string _playerName;

        private void Start()
        {
            _statChanger = new StatChanger(_creatorView.StatView);
            _specializationChanger = new SpecializationChanger(_creatorView.SpecializationView,
                _creatorView.SpecializationConfigsStorage);
            List<СreationTab> creationTabs = new List<СreationTab> {СreationTab.Specialization, СreationTab.Stats};

            _creatorView.Initialize();

            _creationTabsButtons = new Dictionary<CreationTabButton, СreationTab>();

            foreach (var creationTab in creationTabs)
            {
                CreationTabButton tabButton = Instantiate(_creatorView.CreationTabButton, _creatorView.CreationTabsHolder);
                tabButton.Initialize(creationTab.ToString());
                tabButton.OnButtonClicked += TabChanged;
                _creationTabsButtons.Add(tabButton, creationTab);
                if (creationTab == _defaultTab)
                {
                    _currentTabButton = tabButton;
                    tabButton.ChangeState(true);
                }
            }

            _creatorView.OnStartClicked += StartClicked;
            _creatorView.OnNameChanged += NameChanged;

            _currentController = GetAndInitializeController(СreationTab.Specialization);
        }

        private void TabChanged(CreationTabButton creationTabButton)
        {
            _currentTabButton.ChangeState(false); //Deselect previous button
            _currentTabButton = creationTabButton;
            _currentTabButton.ChangeState(true); //Select new button

            СreationTab creationTab = _creationTabsButtons[creationTabButton];
            _currentController?.Complete();
            _currentController = GetAndInitializeController(creationTab);
        }

        private void NameChanged(string playerName)
        {
            if (playerName == null || playerName == _playerName)
            {
                return;
            }

            _playerName = playerName;
        }


        private IViewController GetAndInitializeController(СreationTab creationTab)
        {
            switch (creationTab)
            {
                case СreationTab.Specialization:
                    _specializationChanger.Initialize();
                    return _specializationChanger;
                case СreationTab.Stats:
                {
                    StatsModel model = new StatsModel(_specializationChanger.SpecializationModel.Stats, 10);
                    _statChanger.Initialize(model);
                }
                    return _statChanger;
                default:
                    return null;
            }
        }

        private void OnDestroy()
        {
            foreach (var tabButton in _creationTabsButtons.Keys)
            {
                tabButton.OnButtonClicked -= TabChanged;
            }

            _creatorView.OnStartClicked -= StartClicked;
            _creatorView.OnNameChanged -= NameChanged;
        }

        private void StartClicked()
        {
            PlayerConfig playerConfig = new PlayerConfig(_playerName, _specializationChanger.SpecializationModel.Stats,
                _specializationChanger.SpecializationModel.SpecializationType, null);
            Serializator.Serializate(playerConfig, Path.Combine(Application.dataPath, "Serialization/PlayerData", "Player_" + playerConfig.Id + ".json"));
        }
    }
}