using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Comunity;
using Tibia.MongoDB;

namespace Tibia.Domain.CharAuctions
{
    public class CharAuction : Entity
    {
        public string Name { get; private set; }

        public string DetailPageLink { get; private set; }

        public int Level { get; private set; }

        public EVocation Vocation { get; private set; }

        public EGender Gender { get; private set; }

        public EWorld World { get; private set; }

        public DateTime AuctionEnd { get; private set; }

        public int CurrentBid { get; private set; }

        public CharAuction(string name, string detailPageLink, int level, EVocation vocation, EGender gender, EWorld world, DateTime auctionEnd, int currentBid)
        {
            Name = name;
            DetailPageLink = detailPageLink;
            Level = level;
            Vocation = vocation;
            Gender = gender;
            World = world;
            AuctionEnd = auctionEnd;
            CurrentBid = currentBid;
        }
    }


}
