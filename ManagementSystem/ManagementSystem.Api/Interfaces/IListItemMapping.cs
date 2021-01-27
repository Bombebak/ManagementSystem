using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IListItemMapping
    {
        List<ListItemDto<long>> MapDtos(IEnumerable<ApplicationProject> source);
        List<ListItemDto<long>> MapDtos(IEnumerable<ApplicationSprint> source);
    }
}
