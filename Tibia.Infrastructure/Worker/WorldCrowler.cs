using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Adapters;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker.Interface;

namespace Tibia.Infrastructure.Worker {
    public class WorldCrowler : IWorldCrowler {

        private readonly IWorldAdapter worldAdapter;
        private readonly IUnitOfWork uow;

        public WorldCrowler(IWorldAdapter _worldAdapter, IUnitOfWork _uow) {
            worldAdapter = _worldAdapter;
            uow = _uow;
        }

        public async Task SyncWorlds() {
            var worldList = await worldAdapter.List();

            worldList.ForEach(world => { uow.WorldRepository.InsertOne(world); });

            await uow.Commit();
        }

    }
}
