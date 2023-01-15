namespace Tibia.Domain.Adapters {
    public interface ICharAuctionSearchPageAdapter {
        Task<List<CharAuction>> List();
    }
}
