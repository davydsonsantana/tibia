using Tibia.Domain.CharAuctions;

namespace Tibia.Infrastructure.Adapters.CharAuctions.Contracts
{
    public interface ICharAuctionSearchPageAdapter
    {
        (CharAuctionSearchPaginationStatus, List<CharAuction>) ListCurrentPage(CharAuctionFilter filter);
    }
}
