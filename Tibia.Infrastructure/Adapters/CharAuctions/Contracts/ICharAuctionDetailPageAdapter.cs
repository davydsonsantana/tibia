using Tibia.Domain.CharAuctions;

namespace Tibia.Infrastructure.Adapters.CharAuctions.Contracts {
    public interface ICharAuctionDetailPageAdapter {
        CharAuction LoadChar(CharAuction character);
    }
}
