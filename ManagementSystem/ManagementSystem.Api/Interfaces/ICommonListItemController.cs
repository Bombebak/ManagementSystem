using ManagementSystem.Api.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ICommonListItemController
    {
        List<ListItemDto<long>> GetProjects();
        List<ListItemDto<long>> GetSprints();
    }
}
