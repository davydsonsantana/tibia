using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.MongoDB {
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : class {
        Task<TEntity> GetById(Guid id);
    }
}
