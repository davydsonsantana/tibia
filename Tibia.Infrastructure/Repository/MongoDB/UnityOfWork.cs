using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Repository;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository.MongoDB {
    public class UnitOfWork : IUnitOfWork {
        public ICharAuctionRepository CharAuctionRepository { get; }
        public IWorldRepository WorldRepository { get; }

        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context, ICharAuctionRepository charAuctionRepository, IWorldRepository worldRepository) {
            _context = context;
            CharAuctionRepository = charAuctionRepository;
            WorldRepository = worldRepository;
        }

        public async Task<bool> Commit() {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
