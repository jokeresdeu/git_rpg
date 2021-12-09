using TMPro;
using UnityEngine;

namespace PlayerCreator.Specialization
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _amount;
        
        public TMP_Text Header => _header;
        public TMP_Text Amount => _amount;
    }
}