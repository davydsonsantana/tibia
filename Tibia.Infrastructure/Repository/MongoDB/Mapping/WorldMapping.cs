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

namespace Tibia.Infrastructure.Repository.MongoDB.Mapping
{
    public class WorldMapping
    {
        public void Map() {
            BsonClassMap.RegisterClassMap<World>(cm =>
            {
                cm.MapIdMember(c => c.Id)
                   .SetIdGenerator(new StringObjectIdGenerator())
                   .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.MapMember(c => c.AdditionalInfo);
                cm.

                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
