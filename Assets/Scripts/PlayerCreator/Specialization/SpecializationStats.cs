using System.Collections.Generic;
using GamePlay;
using Player;

namespace PlayerCreator.Specialization
{
    public class SpecializationStats
    {
        public List<Stat> Stats { get; }
        public SpecializationType SpecializationType { get; }

        public SpecializationStats(List<Stat> stats, SpecializationType specializationType)
        {
            Stats = stats;
            SpecializationType = specializationType;
        }
    }
}