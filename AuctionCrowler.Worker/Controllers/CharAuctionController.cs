using AuctionCrowler.Worker.Job.Interface;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Tibia.Infrastructure.Adapters.CharAuctions;


namespace AuctionCrowler.Worker.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CharAuctionController : ControllerBase {

        private readonly ILogger<CharAuctionController> _logger;
        private readonly ICharAuctionCrowler _charAuctionCrowler;
        
        public CharAuctionController(ILogger<CharAuctionController> logger, ICharAuctionCrowler charAuctionCrowler) {
            _logger = logger;
            _charAuctionCrowler = charAuctionCrowler;            
        }

        [HttpPost]
        public ActionResult Sync() {
            var filter = new CharAuctionFilter();
            filter.SetBattlEye(EAuctionBattlEye.InitiallyProtected);
            filter.SetPvPType(EAuctionPvpTypes.OpenPvP);

            BackgroundJob.Enqueue(() => _charAuctionCrowler.Start(filter));
            
            return Ok();
        }
    }
}