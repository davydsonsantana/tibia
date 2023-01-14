using Microsoft.AspNetCore.Mvc;
using Tibia.Domain.Adapters;
using Tibia.Infrastructure.Adapters;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker;
using Tibia.Infrastructure.Worker.Interface;
using Tibia.MongoDB;

namespace AuctionCrowler.Api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorldController : ControllerBase {

        private readonly ILogger<WorldController> logger;
        private readonly IWorldCrowler worldCrowler;
        private readonly IUnitOfWork uow;

        public WorldController(ILogger<WorldController> _logger, IWorldCrowler _worldCrowler , IUnitOfWork _uow) {
            logger = _logger;
            worldCrowler = _worldCrowler;
            uow = _uow;
        }

        [HttpPost(Name = "Sync")]
        public async Task<ActionResult> Sync() {
            _ = worldCrowler.SyncWorlds();
            return Ok();
        }
    }
}