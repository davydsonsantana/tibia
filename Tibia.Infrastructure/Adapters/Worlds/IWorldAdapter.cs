using Tibia.Domain.Comunity;

namespace Tibia.Infrastructure.Adapters.Worlds {
    public interface IWorldAdapter {
        Task<List<World>> List();
    }
}
