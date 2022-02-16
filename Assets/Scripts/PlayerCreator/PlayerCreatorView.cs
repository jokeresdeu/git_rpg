using PlayerCreator.Appearance;
using PlayerCreator.Specialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator
{
    public class PlayerCreatorView : MonoBehaviour
    {
        [SerializeField] private GameObject _creationTabButtonPrefab;
        [SerializeField] private Transform _creationTabsButtonsContainer;
        [SerializeField] private AppearanceView _appearanceView;
        [SerializeField] private SpecializationView _specializationView;
        [SerializeField] private StatView _statView;
        [SerializeField] private Button _saveButton;
        [SerializeField] private TMP_InputField _nameInputField;
    }
}