using AuctionCrowler.Worker.Job.Interface;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Repository;
using Tibia.Infrastructure.Adapters.CharAuctions;
using Tibia.Infrastructure.Repository;
using Tibia.Infrastructure.Repository.MongoDB;

namespace AuctionCrowler.Worker.Job {
    public class CharAuctionCrowler : ICharAuctionCrowler {

        private readonly ICharAuctionSearchPageAdapter _charAuctionSearchPageAdapter;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CharAuctionCrowler> _logger;

        public CharAuctionCrowler(ILogger<CharAuctionCrowler> logger, ICharAuctionSearchPageAdapter charAuctionSearchPageAdapter, IUnitOfWork uow) {
            _charAuctionSearchPageAdapter = charAuctionSearchPageAdapter;
            _uow = uow;
            _logger = logger;
        }

        public async Task Start(CharAuctionFilter filter) {

            var hasMorePages = true;
            while(hasMorePages) {
                hasMorePages = CrowlerPage(filter);
                filter.BumpCurrentPage();
            }

        }

        public bool CrowlerPage(CharAuctionFilter filter) {
            var charAuctionSearchResult = _charAuctionSearchPageAdapter.ListCurrentPage(filter);
            var paginationStatus = charAuctionSearchResult.Item1;
            var auctionList = charAuctionSearchResult.Item2;
            
            foreach (var charAuction in auctionList) {
                SaveAuction(charAuction);
            }            
          
             _uow.Commit();

            return paginationStatus.HasNextPage();
        }

        public void SaveAuction(CharAuction charAuction) {
            var auctionList = _uow.CharAuctionRepository.GetByName(charAuction.Name).Result;
            if (!auctionList.Any()) {
                _uow.CharAuctionRepository.InsertOne(charAuction);
                _logger.LogInformation($"Auction - New char auction saved. Char: {charAuction.Name}");
            }
        }
    }
}
