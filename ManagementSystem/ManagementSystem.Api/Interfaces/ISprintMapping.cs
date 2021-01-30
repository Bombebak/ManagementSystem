using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ISprintMapping
    {
        List<SprintListViewModel> MapToSprintList(List<ApplicationSprint> source);
        SaveSprintRequestViewModel MapViewModelFromDatalayer(ApplicationSprint source);
        ApplicationSprint MapDatalayerFromViewModel(ApplicationSprint target, SaveSprintRequestViewModel source);
    }
}
