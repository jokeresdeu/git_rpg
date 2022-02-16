using System;
using System.Collections.Generic;
using GamePlay;

namespace PlayerCreator.Stats
{
    [Serializable]
    public class StatsModel
    {
        private List<Stat> _stats;
        private int _freeStats;

        public List<Stat> Stats => _stats;
        public int FreeStats => _freeStats;
    }
}