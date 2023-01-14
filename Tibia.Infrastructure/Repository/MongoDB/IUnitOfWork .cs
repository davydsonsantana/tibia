using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Repository;

namespace Tibia.Infrastructure.Repository.MongoDB {
    public interface IUnitOfWork : IDisposable {

        IWorldRepository WorldRepository { get; }
        Task<bool> Commit();
    }
}
