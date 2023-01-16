using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Comunity;

namespace Tibia.Infrastructure.Repository.MongoDB.Mapper {
    public class CharAuctionMapper {
        public static void Configure() {
            BsonClassMap.RegisterClassMap<CharAuction>(map => {
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.DetailPageLink).SetIsRequired(true);
                map.MapMember(x => x.Level).SetIsRequired(true);
                map.MapMember(x => x.Vocation).SetIsRequired(true);
                map.MapMember(x => x.Gender).SetIsRequired(true);
                map.MapMember(x => x.World).SetIsRequired(true);
                map.MapMember(x => x.AuctionEnd).SetIsRequired(true);
                map.MapMember(x => x.CurrentBid).SetIsRequired(true);
                
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
