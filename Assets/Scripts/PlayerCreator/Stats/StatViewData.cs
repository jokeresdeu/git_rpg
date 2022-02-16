using GamePlay;

namespace PlayerCreator.Stats
{
    public class StatViewData
    {
        public StatView StatView { get; }
        public Stat Stat { get; }
        public int MinValue { get; }

        public StatViewData(StatView statView, Stat stat, int minValue)
        {
            StatView = statView;
            Stat = stat;
            MinValue = minValue;
        }
    }
}