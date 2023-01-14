using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker.Interface;

namespace AuctionCrowler.Worker.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorldController : ControllerBase {

        private readonly ILogger<WorldController> logger;
        private readonly IWorldCrowler worldCrowler;
        private readonly IUnitOfWork uow;

        public WorldController(ILogger<WorldController> _logger, IWorldCrowler _worldCrowler, IUnitOfWork _uow) {
            logger = _logger;
            worldCrowler = _worldCrowler;
            uow = _uow;
        }

        [HttpPost(Name = "Sync")]
        public ActionResult Sync() {
            BackgroundJob.Enqueue(() => worldCrowler.SyncWorlds());            
            return Ok();
        }
    }
}