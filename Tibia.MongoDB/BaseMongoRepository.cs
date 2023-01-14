using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.MongoDB {
    public abstract class BaseRepository<TEntity> : IMongoRepository<TEntity> where TEntity : Entity {

        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public BaseRepository(IMongoContext context) {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        public virtual void InsertOne(TEntity obj) {
            Context.AddCommand(() => DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetById(Guid id) {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual void Remove(Guid id) {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose() {
            Context?.Dispose();
        }
    }
}
