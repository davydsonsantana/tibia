using Tibia.Domain.Comunity;

namespace Tibia.Domain.Adapters
{
    public interface IWorldAdapter
    {
        Task<List<World>> List();
    }
}
