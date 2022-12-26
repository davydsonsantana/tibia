﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Comunity;

namespace Tibia.Domain
{
    internal class Auction
    {
        internal EAuctionType Type { get; private set; }
        internal IAuctionable Item { get; private set; }

        internal Auction(IAuctionable item)
        {
            Item = item;
            DefineType(item);
        }

        private void DefineType(IAuctionable item)
        {
            Type = item switch
            {
                Character => EAuctionType.Character,
                _ => throw new ArgumentException("Invalid item type")
            };
        }
    }


}