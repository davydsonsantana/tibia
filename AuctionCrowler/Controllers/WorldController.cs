using Microsoft.AspNetCore.Mvc;
using Tibia.Infrastructure.Worker;

namespace AuctionCrowler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorldController : ControllerBase {

        private readonly IWorldCrowler worldCrowler;
        public WorldController(IWorldCrowler _worldCrowler) {
            worldCrowler = _worldCrowler;
        }

        [HttpPost]
        [Route("Sync")]
        public async Task<ActionResult> Sync() {
            _ = Task.Run(worldCrowler.SyncWorlds);
            return Accepted();
        }

    }
}
