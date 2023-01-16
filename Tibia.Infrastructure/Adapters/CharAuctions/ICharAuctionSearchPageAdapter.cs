using Tibia.Domain.CharAuctions;

namespace Tibia.Infrastructure.Adapters.CharAuctions
{
    public interface ICharAuctionSearchPageAdapter {
        Task<(CharAuctionSearchPaginationStatus, List<CharAuction>)> ListCurrentPage(CharAuctionFilter filter);
    }
}
