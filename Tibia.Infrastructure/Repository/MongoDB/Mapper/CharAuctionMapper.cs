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
                map.MapMember(x => x.Status).SetIsRequired(false);
                map.MapMember(x => x.ItsDetailed).SetIsRequired(false);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.DetailPageLink).SetIsRequired(true);
                map.MapMember(x => x.Level).SetIsRequired(true);
                map.MapMember(x => x.Vocation).SetIsRequired(true);
                map.MapMember(x => x.Gender).SetIsRequired(true);
                map.MapMember(x => x.World).SetIsRequired(true);
                map.MapMember(x => x.AuctionStart).SetIsRequired(true);
                map.MapMember(x => x.AuctionEnd).SetIsRequired(true);
                map.MapMember(x => x.CurrentBid).SetIsRequired(true);
                map.MapMember(x => x.HitPoints).SetIsRequired(false);
                map.MapMember(x => x.Mana).SetIsRequired(false);
                map.MapMember(x => x.Capacity).SetIsRequired(false);
                map.MapMember(x => x.Speed).SetIsRequired(false);
                map.MapMember(x => x.Blessings).SetIsRequired(false);
                map.MapMember(x => x.Mounts).SetIsRequired(false);
                map.MapMember(x => x.Outfits).SetIsRequired(false);
                map.MapMember(x => x.Titles).SetIsRequired(false);
                map.MapMember(x => x.AxeFighting).SetIsRequired(false);
                map.MapMember(x => x.ClubFighting).SetIsRequired(false);
                map.MapMember(x => x.DistanceFighting).SetIsRequired(false);
                map.MapMember(x => x.Fishing).SetIsRequired(false);
                map.MapMember(x => x.FistFighting).SetIsRequired(false);
                map.MapMember(x => x.MagicLevel).SetIsRequired(false);
                map.MapMember(x => x.Shielding).SetIsRequired(false);
                map.MapMember(x => x.SwordFighting).SetIsRequired(false);

                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
