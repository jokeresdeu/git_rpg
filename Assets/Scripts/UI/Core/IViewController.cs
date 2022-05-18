namespace UI.Core
{
    public interface IViewController
    {
        void Initialize(params object[] args);
        void Complete();
    }
}