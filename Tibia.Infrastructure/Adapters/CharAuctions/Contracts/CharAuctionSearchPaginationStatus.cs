using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia.Infrastructure.Adapters.CharAuctions.Contracts
{

    public class CharAuctionSearchPaginationStatus
    {

        public int CurrentPage { get; private set; }
        public int TotalResult { get; private set; }
        public int PageResultSize { get; private set; }
        public int PageResultCount { get; private set; }

        public CharAuctionSearchPaginationStatus(int currentPage, int totalResult, int pageResultCount)
        {
            CurrentPage = currentPage;
            TotalResult = totalResult;
            PageResultCount = pageResultCount;
            PageResultSize = 25;
        }

        public bool HasNextPage() =>
            TotalResult > CurrentPage * PageResultSize;

        public int GetNextPage() =>
            CurrentPage + 1;

    }
}
