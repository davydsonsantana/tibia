using AuctionCrowler.Worker.Job.Interface;
using Hangfire;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.DevTools.V106.Target;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Repository;
using Tibia.Infrastructure.Adapters.CharAuctions.Contracts;
using Tibia.Infrastructure.Repository;
using Tibia.Infrastructure.Repository.MongoDB;

namespace AuctionCrowler.Worker.Job {
    public class CharAuctionCrowler : ICharAuctionCrowler {

        private readonly ICharAuctionSearchPageAdapter _charAuctionSearchPageAdapter;
        private readonly ICharAuctionDetailPageAdapter _charAuctionDetailPageAdapter;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CharAuctionCrowler> _logger;

        public CharAuctionCrowler(ILogger<CharAuctionCrowler> logger, ICharAuctionSearchPageAdapter charAuctionSearchPageAdapter,
            ICharAuctionDetailPageAdapter charAuctionDetailPageAdapter, IUnitOfWork uow) {
            _charAuctionSearchPageAdapter = charAuctionSearchPageAdapter;
            _charAuctionDetailPageAdapter = charAuctionDetailPageAdapter;
            _uow = uow;
            _logger = logger;
        }
        public async Task StartCrowlerSearchPage(CharAuctionFilter filter) {
            var hasMorePages = true;
            while (hasMorePages) {
                hasMorePages = CrowlerPage(filter);
                filter.BumpCurrentPage();
            }
        }

        private bool CrowlerPage(CharAuctionFilter filter) {
            var charAuctionSearchResult = _charAuctionSearchPageAdapter.ListCurrentPage(filter);
            var paginationStatus = charAuctionSearchResult.Item1;
            var auctionList = charAuctionSearchResult.Item2;

            var storedAuctionList = _uow.CharAuctionRepository.GetByNames(auctionList.Select(a => a.Name).ToList()).Result;

            auctionList.ForEach(auction => {

                if (!storedAuctionList.Any(c => c.Name == auction.Name)) {
                    _uow.CharAuctionRepository.InsertOne(auction);
                    _logger.LogInformation($"Auction - New char auction saved. Char: {auction.Name}");
                } else {
                    _logger.LogWarning($"Auction - Already stored. Char: {auction.Name}");
                }
            });

            _uow.Commit();

            return paginationStatus.HasNextPage();
        }

        public async Task StartCrowlerUpdateChar() {
            var charAuctionList = _uow.CharAuctionRepository.GetToUpdate().Result.ToList();

            charAuctionList.ForEach(charAuction => {
                var charUpdated = _charAuctionDetailPageAdapter.LoadChar(charAuction);
                var dbResponse = _uow.CharAuctionRepository.Update(charUpdated);

                if (dbResponse.IsFaulted) {
                    _logger.LogError($"Erro to update CharAuction '{charAuction.Name}'. MSG: { dbResponse.Exception.Message}");
                    throw dbResponse.Exception;
                }

                Thread.Sleep(new Random().Next(800, 3000));
            });
        }

    }
}
