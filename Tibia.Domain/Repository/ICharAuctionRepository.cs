﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Comunity;
using Tibia.MongoDB;

namespace Tibia.Domain.Repository {
    public interface ICharAuctionRepository : IMongoRepository<CharAuction> {

    }
}
