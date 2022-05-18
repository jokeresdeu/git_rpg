using System;
using GamePlay.Data;
using Player.Enum;
using UI.PlayerCreator.Appearance.Data;
using UI.PlayerCreator.Appearance.View;

namespace UI.PlayerCreator.Appearance.Controller
{
    public class AppearanceElementController
    {
        private readonly AppearanceElementView _view;
        private readonly AppearanceFeatureSprites _appearanceFeatureSprites;
        private int _index;

        public event Action<AppearanceFeature, DirectionalSpritesContainer> OnAppearanceFeatureSpritesChanged;

        public AppearanceElementController(AppearanceElementView view, AppearanceFeatureSprites appearanceFeatureSprites)
        {
            _view = view;
            _appearanceFeatureSprites = appearanceFeatureSprites;
            _view.ElementHeader.text = _appearanceFeatureSprites.AppearanceFeature.ToString();
            _view.Show();
            _view.OnNextClicked += NextClicked;
            _view.OnPrevClicked += PrevClicked;
            _view.StyleHeader.text = $"style_{_index}";
        }

        private void NextClicked()
        {
            _index++;
            if (_index > _appearanceFeatureSprites.DirectionalSpritesCollection.Count - 1)
            {
                _index = 0;
            }

            ChangeAppearanceFeatureSprite();
        }

        private void PrevClicked()
        {
            _index--;
            if (_index < 0)
            {
                _index = _appearanceFeatureSprites.DirectionalSpritesCollection.Count - 1;
            }

            ChangeAppearanceFeatureSprite();
        }

        private void ChangeAppearanceFeatureSprite()
        {
            _view.StyleHeader.text = $"style_{_index}";
            OnAppearanceFeatureSpritesChanged?.Invoke(_appearanceFeatureSprites.AppearanceFeature,
                _appearanceFeatureSprites.DirectionalSpritesCollection[_index]);
        }

        public void Dispose()
        {
            _view.Hide();
            _view.OnNextClicked -= NextClicked;
            _view.OnPrevClicked -= PrevClicked;
        }
    }
}