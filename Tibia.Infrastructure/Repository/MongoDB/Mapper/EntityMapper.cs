using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Tibia.Domain.Comunity;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository.MongoDB.Mapper {
    public class EntityMapper {
        public static void Configure() {
            BsonClassMap.RegisterClassMap<Entity>(map => {                                
                map.MapIdMember(c => c.Id)
                   .SetIdGenerator(new StringObjectIdGenerator())
                   .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
