using System;
using System.Collections.Generic;
using GamePlay;

namespace PlayerCreator.Stats
{
    [Serializable]
    public class StatsModel
    {
        public int FreeStats { get; set; }
        public List<Stat> Stats { get; }
        public StatsModel(List<Stat> stats,  int freeStats)
        {
            Stats = new List<Stat>();
            foreach (var stat in stats)
            {
                Stats.Add(stat.Clone());
            }
            FreeStats = freeStats;
        }
    }
}