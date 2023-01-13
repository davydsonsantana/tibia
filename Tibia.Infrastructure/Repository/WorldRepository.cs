using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Comunity;
using Tibia.Domain.Repository;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository
{
    public class WorldRepository : BaseRepository<World>, IWorldRepository {        
        public WorldRepository(IMongoContext context) : base(context) {
        }  
    }
}
