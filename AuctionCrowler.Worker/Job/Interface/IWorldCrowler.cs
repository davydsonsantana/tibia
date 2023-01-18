using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionCrowler.Worker.Job.Interface {
    public interface IWorldCrowler {
        Task SyncWorlds();
    }
}
