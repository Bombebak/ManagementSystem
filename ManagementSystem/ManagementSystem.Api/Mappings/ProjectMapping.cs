using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class ProjectMapping : IProjectMapping
    {
        public List<ProjectListViewModel> MapToProjectList(List<ApplicationProject> source)
        {
            var result = new List<ProjectListViewModel>();

            if (source == null)
            {
                return result;
            }

            result.AddRange(source.Select(e => MapToProject(e)));

            return result;
        }

        public SaveProjectRequestViewModel MapViewModelFromDatalayer(ApplicationProject source)
        {
            if (source == null)
            {
                return null;
            }

            return new SaveProjectRequestViewModel
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public ApplicationProject MapDatalayerFromViewModel(ApplicationProject target, SaveProjectRequestViewModel source)
        {
            if (target == null)
            {
                target = new ApplicationProject();
            }

            target.Name = source.Name;

            return target;
        }

        private ProjectListViewModel MapToProject(ApplicationProject source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new ProjectListViewModel();
            target.Id = source.Id;
            target.Name = source.Name;

            return target;
        }


    }
}
