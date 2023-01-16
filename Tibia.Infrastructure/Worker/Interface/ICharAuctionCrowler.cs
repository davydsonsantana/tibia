using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Infrastructure.Adapters.CharAuctions;

namespace Tibia.Infrastructure.Worker.Interface {
    public interface ICharAuctionCrowler {
        void Start(CharAuctionFilter filter);
    }
}
