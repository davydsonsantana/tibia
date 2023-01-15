using Tibia.Domain.Comunity;

namespace Tibia.Domain.Adapters
{
    public interface ICharAuctionSearchPageAdapter {
        Task<List<Auction>> List();
    }
}
