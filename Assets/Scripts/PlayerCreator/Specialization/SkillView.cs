using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Specialization
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;
        
        public TMP_Text Header => _header;
        public Image Icon => _icon;
        public TMP_Text Description => _description;
    }
}