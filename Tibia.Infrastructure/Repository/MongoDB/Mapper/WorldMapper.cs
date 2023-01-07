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

namespace Tibia.Infrastructure.Repository.MongoDB.Mapper {
    public class WorldMapper {
        public static void Configure() {
            BsonClassMap.RegisterClassMap<World>(map => {
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Location).SetIsRequired(true);
                map.MapMember(x => x.PvpType).SetIsRequired(true);
                map.MapMember(x => x.BattleEye).SetIsRequired(true);
                map.MapMember(x => x.AdditionalInfo).SetIsRequired(true);

                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
