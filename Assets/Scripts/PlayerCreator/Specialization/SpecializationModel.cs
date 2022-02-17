using System.Collections.Generic;
using GamePlay;
using Player;

namespace PlayerCreator.Specialization
{
    public class SpecializationModel
    {
        public List<Stat> Stats { get;  }

        public SpecializationType SpecializationType { get; private set; }

        public SpecializationModel(List<Stat> stats)
        {
            Stats = stats;
        }

        public void ChangeSpecialization(SpecializationType specializationType, List<Stat> stats)
        {
            SpecializationType = specializationType;
            Stats.Clear();
            foreach (var stat in stats)
            {
                Stats.Add(new Stat(stat.StatType, stat.Value));
            }
        }
    }
}