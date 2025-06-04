using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;

namespace YourTrips.Core.Interfaces.Admin
{
    public interface IUserSortingService
    {

        Task<SortingComparisonResult> CompareSortingMethods();
    }
}
