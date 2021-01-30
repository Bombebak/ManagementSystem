using ManagementSystem.Api.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ICommonListItemController
    {
        Task<List<ListItemDto<long>>> GetProjectsAsync(bool includeDefault);
        Task<List<ListItemDto<long>>> GetSprintsAsync(bool includeDefault);
    }
}
