using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Adapters;

namespace Tibia.Infrastructure.Worker {
    public class WorldCrowler : IWorldCrowler {

        private readonly IWorldAdapter worldAdapter;

        public WorldCrowler(IWorldAdapter _worldAdapter) {
            worldAdapter = _worldAdapter;
        }

        public async Task SyncWorlds() {
            var worldList = await worldAdapter.List();
        }

    }
}
