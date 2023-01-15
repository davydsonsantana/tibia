using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.MongoDB;

namespace Tibia.Domain.Comunity
{
    public class World : Entity
    {
        public string Name { get; private set; }
        public ELocation Location { get; private set; }
        public EPvpType PvpType { get; private set; }
        public EBattlEye BattlEye { get; private set; }
        public string AdditionalInfo { get; private set; }

        public World(string name, ELocation location, EPvpType pvpType, EBattlEye battlEye, string additionalInfo = "")
        {
            Name = name;
            Location = location;
            PvpType = pvpType;
            BattlEye = battlEye;
            AdditionalInfo = additionalInfo;
        }
    }
}
