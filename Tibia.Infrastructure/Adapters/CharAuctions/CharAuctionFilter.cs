using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.Infrastructure.Adapters.CharAuctions {
    public class CharAuctionFilter {

        private readonly string AuctionSearchPageURI = "https://www.tibia.com/charactertrade/?subtopic=currentcharactertrades";
        public EAuctionWorld World { get; private set; }
        public EAuctionPvpTypes PvPType { get; private set; }
        public EAuctionBattlEye BattlEye { get; private set; }
        public EAuctionVocation Vocation { get; private set; }
        public int LevelRangeFrom { get; private set; }
        public int LevelRangeTo { get; private set; }
        public int CurrentPage { get; private set; }

        public CharAuctionFilter() {
            World = EAuctionWorld.ALL_WORLDS;
            PvPType = EAuctionPvpTypes.ALL_PVP_TYPES;
            BattlEye = EAuctionBattlEye.ALL_BATTLEYE;
            Vocation = EAuctionVocation.ALL_VOCATIONS;
            LevelRangeFrom = 0;
            LevelRangeTo = 0;
            CurrentPage = 1;
        }

        public void SetWorld(EAuctionWorld world) {
            World = world;
        }

        public void SetPvPType(EAuctionPvpTypes pvpType) {
            PvPType = pvpType;
        }

        public void SetBattlEye(EAuctionBattlEye battleye) {
            BattlEye = battleye;
        }

        public void SetVocation(EAuctionVocation vocation) {
            Vocation = vocation;
        }

        public void BumpCurrentPage() {
            CurrentPage += 1;
        }

        public string BuildURI() {
            StringBuilder sb = new StringBuilder();
            sb.Append(AuctionSearchPageURI);
            sb.Append($"&filter_profession={(int)Vocation}");
            sb.Append($"&filter_levelrangefrom={(int)LevelRangeFrom}");
            sb.Append($"&filter_levelrangeto={(int)LevelRangeTo}");
            sb.Append($"&filter_levelrangeto={(int)LevelRangeTo}");
            sb.Append(GetWorldFilter());
            sb.Append($"&filter_worldpvptype={(int)PvPType}");
            sb.Append($"&filter_worldbattleyestate={(int)BattlEye}");
            sb.Append($"&filter_skillid=");
            sb.Append($"&filter_skillrangefrom=0");
            sb.Append($"&filter_skillrangeto=0");
            sb.Append($"&order_column=101");
            sb.Append($"&order_direction=1");
            sb.Append($"&searchtype=1");
            sb.Append($"&currentpage={CurrentPage}");
            return sb.ToString();
        }

        private string GetWorldFilter() {
            if (World == EAuctionWorld.ALL_WORLDS) {
                return "&filter_world=";
            } else {
                return $"&filter_world={ World.ToString() }";
            }
        }                
    }
}
