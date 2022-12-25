using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.Domain.Comunity
{
    public class Character : IAuctionable
    {
        public string Name { get; private set; }
        public World World { get; private set; }

        public Character(string name, World world)
        {
            Name = name;
            World = world;
        }
    }
}
