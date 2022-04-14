using System.Collections.Generic;
using GamePlay;
using Player;

namespace PlayerCreator.Specialization
{
    public class SpecializationModel
    {
        public List<Stat> Stats { get;  }
        public SpecializationType SpecializationType { get;  }

        public SpecializationModel(SpecializationType specializationType, List<Stat> stats)
        {
            SpecializationType = specializationType;
            Stats = new List<Stat>();
            foreach (var stat in stats)
            {
                Stats.Add(stat.Clone());
            }
        }
    }
}