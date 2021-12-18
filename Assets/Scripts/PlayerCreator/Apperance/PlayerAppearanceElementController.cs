using UnityEngine;

namespace PlayerCreator
{
    public class PlayerAppearanceElementController
    {
        private PlayerAppearanceElementView _view;
        private AppearanceFeatureSprites _appearanceFeatureSprites;
        private SpriteRenderer _spriteRenderer;
        private int _index;

        public int Index => _index;
        public AppearanceFeature Feature => _appearanceFeatureSprites.AppearanceFeature;
        
        public PlayerAppearanceElementController(PlayerAppearanceElementView view,
            AppearanceFeatureSprites featureSprites, SpriteRenderer spriteRenderer, int index)
        {
            _index = index;
            _view = view;
            _appearanceFeatureSprites = featureSprites;
            _spriteRenderer = spriteRenderer;
            _view.ElementHeader.text = _appearanceFeatureSprites.AppearanceFeature.ToString();
            _view.RightArrow.onClick.AddListener(NextElement);
            _view.LeftArrow.onClick.AddListener(PreviousElement);
            ChangeAppearanceElement();
        }

        private void NextElement()
        {
            _index++;
            if (_index > _appearanceFeatureSprites.Sprites.Count - 1)
            {
                _index = 0;
            }
            ChangeAppearanceElement();
        }

        private void PreviousElement()
        {
            _index--;
            if (_index < 0)
            {
                _index = _appearanceFeatureSprites.Sprites.Count - 1;
            }
            ChangeAppearanceElement();
        }

        private void ChangeAppearanceElement()
        {
            _spriteRenderer.sprite = _appearanceFeatureSprites.Sprites[_index];
            _view.StyleHeader.text = $"style_{_index}";
        }

        public void Dispose()
        {
            _view.RightArrow.onClick.RemoveListener(NextElement);
            _view.LeftArrow.onClick.RemoveListener(PreviousElement);
        }
    }
}