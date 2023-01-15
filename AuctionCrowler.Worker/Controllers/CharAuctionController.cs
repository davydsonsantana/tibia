using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Tibia.Domain.Adapters;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker.Interface;

namespace AuctionCrowler.Worker.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CharAuctionController : ControllerBase {

        private readonly ILogger<CharAuctionController> _logger;
        private readonly ICharAuctionSearchPageAdapter _charAuctionSearchPageAdapter;
        private readonly IUnitOfWork _uow;

        public CharAuctionController(ILogger<CharAuctionController> logger, ICharAuctionSearchPageAdapter charAuctionSearchPageAdapter, IUnitOfWork uow) {
            _logger = logger;
            _charAuctionSearchPageAdapter = charAuctionSearchPageAdapter;
            _uow = uow;
        }

        [HttpPost]
        public ActionResult Sync() {
            //BackgroundJob.Enqueue(() => worldCrowler.SyncWorlds());
            var charAuctionList = _charAuctionSearchPageAdapter.List();
            return Ok();
        }
    }
}