using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Infrastructure.Adapters.CharAuctions.Contracts;

namespace AuctionCrowler.Worker.Job.Interface
{
    public interface ICharAuctionCrowler {
        Task StartCrowlerSearchPage(CharAuctionFilter filter);
        Task StartCrowlerUpdateChar();
    }
}
