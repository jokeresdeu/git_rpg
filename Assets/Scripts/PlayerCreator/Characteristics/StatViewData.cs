using GamePlay;

namespace PlayerCreator.Characteristics
{
    public class StatViewData
    {
        public Stat Stat { get; }
        public int MinValue { get; }
        public int MaxValue { get; }
        
        public StatViewData(Stat stat, int minValue, int maxValue)
        {
            Stat = stat;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}