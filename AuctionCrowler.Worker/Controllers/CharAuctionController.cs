using AuctionCrowler.Worker.Job.Interface;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Tibia.Infrastructure.Adapters.CharAuctions;
using Tibia.Infrastructure.Adapters.CharAuctions.Contracts;

namespace AuctionCrowler.Worker.Controllers
{
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
        [Route("CrowlerSearchPage")]
        public ActionResult CrowlerSearchPage() {
            var filter = new CharAuctionFilter();
            filter.SetBattlEye(EAuctionBattlEye.InitiallyProtected);
            filter.SetPvPType(EAuctionPvpTypes.OpenPvP);

            BackgroundJob.Enqueue(() => _charAuctionCrowler.StartCrowlerSearchPage(filter));
            
            return Ok();
        }

        [HttpPost]
        [Route("CrowlerUpdateChar")]
        public ActionResult CrowlerUpdateChar() {

            BackgroundJob.Enqueue(() => _charAuctionCrowler.StartCrowlerUpdateChar());

            return Ok();
        }
    }
}