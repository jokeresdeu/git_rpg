namespace CoreUI
{
    public interface IViewController
    {
        void Initialize(params object[] args);
        void Complete();
    }
}