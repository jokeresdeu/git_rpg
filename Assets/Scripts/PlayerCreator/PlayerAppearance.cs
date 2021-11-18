using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hair;
        [SerializeField] private SpriteRenderer _face;
        [SerializeField] private SpriteRenderer _beard;

        public SpriteRenderer Hair => _hair;
        public SpriteRenderer Face => _face;
        public SpriteRenderer Beard => _beard;
    }
}
