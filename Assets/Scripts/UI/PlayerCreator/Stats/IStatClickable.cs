using System;

namespace UI.PlayerCreator.Stats
{
    public interface IStatClickable
    {
        void Initialize();
        event Action<IStatClickable> OnClicked;
    }
}