using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Comunity;
using Tibia.Domain.Repository;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository {
    public class CharAuctionRepository : BaseRepository<CharAuction>, ICharAuctionRepository {
        public CharAuctionRepository(IMongoContext context) : base(context) {

        }

        public virtual async Task<IList<CharAuction>> GetByName(string name) {
            var data = await DbSet.FindAsync(Builders<CharAuction>.Filter.Eq(x => x.Name, name));
            return data.ToList();
        }
    }
}