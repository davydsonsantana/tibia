using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository.MongoDB
{
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetById(Guid id);
    }
}
