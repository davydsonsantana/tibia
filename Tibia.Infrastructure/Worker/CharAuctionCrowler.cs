using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Infrastructure.Adapters.CharAuctions;
using Tibia.Infrastructure.Worker.Interface;

namespace Tibia.Infrastructure.Worker {
    public class CharAuctionCrowler : ICharAuctionCrowler {

        private readonly ICharAuctionSearchPageAdapter _charAuctionSearchPageAdapter;
        private readonly ILogger<CharAuctionCrowler> _logger;

        public CharAuctionCrowler(ILogger<CharAuctionCrowler> logger, ICharAuctionSearchPageAdapter charAuctionSearchPageAdapter) {
            _charAuctionSearchPageAdapter = charAuctionSearchPageAdapter;
            _logger = logger;
        }

        public void Start(CharAuctionFilter filter) {            
            var list = _charAuctionSearchPageAdapter.ListCurrentPage(filter);
        }
    }
}
