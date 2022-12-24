using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibia.console.Tibia.Comunity
{
    internal class Character : IAuctionable
    {

        internal string Name { get; private set; }
        internal World World { get; private set; }

        public Character(string name, World world)
        {
            Name = name;
            World = world;
        }
    }
}
