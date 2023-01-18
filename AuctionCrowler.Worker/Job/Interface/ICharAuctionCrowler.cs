using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Infrastructure.Adapters.CharAuctions;

namespace AuctionCrowler.Worker.Job.Interface {
    public interface ICharAuctionCrowler {
        Task Start(CharAuctionFilter filter);
    }
}
