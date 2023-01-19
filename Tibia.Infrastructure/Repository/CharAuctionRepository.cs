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

        public virtual async Task<IList<CharAuction>> GetByNames(List<string> names) {
            var data = await DbSet.FindAsync(Builders<CharAuction>.Filter.In(x => x.Name, names));
            return data.ToList();
        }
        public virtual async Task<IList<CharAuction>> GetToUpdate() {
            var data = await DbSet.FindAsync(Builders<CharAuction>.Filter.Eq(x => x.ItsDetailed, false) 
                | Builders<CharAuction>.Filter.Exists(x => x.ItsDetailed, false) 
                | Builders<CharAuction>.Filter.Eq(x => x.Status, EBidStatus.Open)
                | Builders<CharAuction>.Filter.Exists(x => x.Status, false));
            return data.ToList();
        }

        public virtual async Task<ReplaceOneResult> Update(CharAuction charAuction) {
            return await DbSet.ReplaceOneAsync<CharAuction>(c => c.Id == charAuction.Id, charAuction);            
        }
    }
}