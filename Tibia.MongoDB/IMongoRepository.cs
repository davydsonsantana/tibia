using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.MongoDB {
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : Entity {
        void InsertOne(TEntity obj);
        Task<TEntity> GetById(Guid id);
        void Remove(Guid id);
    }
}
