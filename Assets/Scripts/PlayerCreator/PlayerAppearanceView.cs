using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearanceView : MonoBehaviour
    {
        [SerializeField] private PlayerAppearanceElementView _appearanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public PlayerAppearanceElementView PlayerAppearanceElementView => _appearanceElementView;
        public Transform ElementGrid => _elementsGrid;
    }
}
