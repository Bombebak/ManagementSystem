using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IProjectMapping
    {
        List<ProjectListViewModel> MapToProjectList(List<ApplicationProject> source);
        SaveProjectRequestViewModel MapViewModelFromDatalayer(ApplicationProject source);
        ApplicationProject MapDatalayerFromViewModel(ApplicationProject target, SaveProjectRequestViewModel source);
    }
}
