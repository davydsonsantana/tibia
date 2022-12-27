using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.MongoDB
{
    public abstract class Entity
    {
        public string Id { get; private set; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
