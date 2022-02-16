using UnityEngine;

namespace PlayerCreator.Appearance
{
    public class AppearanceView : MonoBehaviour
    {
        [SerializeField] private AppearanceElementView _appearanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public AppearanceElementView AppearanceElementView => _appearanceElementView;
        public Transform ElementGrid => _elementsGrid;
    }
}
