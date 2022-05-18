using GamePlay;

namespace UI.PlayerCreator.Stats
{
    public class StatControllerData
    {
        public StatController StatController { get; }
        public Stat Stat { get; }
        public int MinValue { get; }

        public StatControllerData(StatController statController, Stat stat, int minValue)
        {
            StatController = statController;
            Stat = stat;
            MinValue = minValue;
        }
    }
}