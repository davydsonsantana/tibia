using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Comunity;

namespace Tibia.Infrastructure.Repository
{
    public  class WorldRepository
    {
        private readonly IMongoCollection<World> _world;

        public WorldRepository(IMongoClient client)
        {
            var database = client.GetDatabase("tibia");
            _world = database.GetCollection<World>(nameof(World));
        }
    }
}
