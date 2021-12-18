using TMPro;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statType;
        [SerializeField] private TMP_Text _statAmount;
        
        public TMP_Text StatType => _statType;
        public TMP_Text StatAmount => _statAmount;
    }
}