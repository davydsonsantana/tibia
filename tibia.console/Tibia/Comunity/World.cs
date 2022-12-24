using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibia.console.Tibia.Comunity {
    internal class World {
        internal string Name { get; set; }
        internal ELocation Location { get; set; }
        internal EPvpType PvpType { get; set; }
        internal EBattleEye BattleEye { get; set; }


        public World(string name, ELocation location, EPvpType pvpType, EBattleEye battleEye) {
            Name = name;
            Location = location;
            PvpType = pvpType;
            BattleEye = battleEye;
            Console.WriteLine($"World: {Name}");
        }
    }
}
