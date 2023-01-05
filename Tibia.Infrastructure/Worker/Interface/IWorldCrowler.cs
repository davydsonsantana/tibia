using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.Infrastructure.Worker.Interface {
    public interface IWorldCrowler {
        Task SyncWorlds();
    }
}
