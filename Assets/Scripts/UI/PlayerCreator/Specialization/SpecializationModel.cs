using System.Collections.Generic;
using GamePlay;
using Player;

namespace UI.PlayerCreator.Specialization
{
    public class SpecializationModel
    {
        public int FreeStats { get; }
        public List<Stat> DefaultStats { get; }
        public List<Stat> Stats { get;  }
        public SpecializationType SpecializationType { get;  }
        
        public SpecializationModel(SpecializationType specializationType, List<Stat> stats, List<Stat> defaultStats, int freeStats)
        {
            SpecializationType = specializationType;
            Stats = new List<Stat>();
            foreach (var stat in stats)
            {
                Stats.Add(stat.Clone());
            }

            FreeStats = freeStats;
            DefaultStats = defaultStats;
        }
    }
}