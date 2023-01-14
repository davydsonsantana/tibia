using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Adapters;
using Tibia.Domain.Comunity;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker.Interface;

namespace Tibia.Infrastructure.Worker {
    public class WorldCrowler : IWorldCrowler {

        private readonly IWorldAdapter _worldAdapter;
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;

        public WorldCrowler(IWorldAdapter worldAdapter, IUnitOfWork uow, ILogger<WorldCrowler> logger) {
            _worldAdapter = worldAdapter;
            _uow = uow;
            _logger = logger;
        }

        public async Task SyncWorlds() {
            var worldListTibiaPortal = await _worldAdapter.List();
            var worldListSaved = await _uow.WorldRepository.GetAll();

            worldListTibiaPortal.ForEach(world => {
                if (!worldListSaved.Any(w => w.Name == world.Name)) {
                    _uow.WorldRepository.InsertOne(world);
                    _logger.LogInformation($"New world '{world.Name.ToUpper()}' found. Saving...");
                }
            });

            await _uow.Commit();
            _logger.LogInformation($"World Sync completed");
        }

    }
}
