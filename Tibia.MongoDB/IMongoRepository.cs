using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.MongoDB {
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : Entity {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        void InsertOne(TEntity obj);
        void Remove(Guid id);
    }
}
