using Tibia.Domain.CharAuctions;

namespace Tibia.Infrastructure.Adapters.CharAuctions
{
    public interface ICharAuctionSearchPageAdapter {
        (CharAuctionSearchPaginationStatus, List<CharAuction>) ListCurrentPage(CharAuctionFilter filter);
    }
}
