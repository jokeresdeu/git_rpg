using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Specialization
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _skillName;
        [SerializeField] private TMP_Text _skillDescription;
        [SerializeField] private Image _skillImage;
        
        public TMP_Text SkillName => _skillName;
        public TMP_Text SkillDescription => _skillDescription;
        public Image SkillImage => _skillImage;
    }
}