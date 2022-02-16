using System;

namespace PlayerCreator.Stats
{
    public interface IStatClickable
    {
        void Initialize();
        event Action<IStatClickable> OnClicked;
    }
}