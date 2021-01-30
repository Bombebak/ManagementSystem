using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Sprint;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class SprintMapping : ISprintMapping
    {
        private readonly ILogger<SprintMapping> _logger;

        public SaveSprintRequestViewModel MapViewModelFromDatalayer(ApplicationSprint source)
        {
            if (source == null)
            {
                return null;
            }

            return new SaveSprintRequestViewModel
            {
                Id = source.Id,
                Name = source.Name,
                //Description = source.Description,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };
        }

        public ApplicationSprint MapDatalayerFromViewModel(ApplicationSprint target, SaveSprintRequestViewModel source)
        {
            if (target == null)
            {
                target = new ApplicationSprint();
            }

            target.Name = source.Name;
            //target.Description = source.Description;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;

            return target;
        }

        public List<SprintListViewModel> MapToSprintList(List<ApplicationSprint> source)
        {
            var result = new List<SprintListViewModel>();
            if (source == null)
            {
                return result;
            }

            result.AddRange(source.Select(e => MapToSprintList(e)));

            return result;
        }

        private SprintListViewModel MapToSprintList(ApplicationSprint source)
        {
            if (source == null)
            {
                return null;
            }
            return new SprintListViewModel
            {
                Id = source.Id,
                Name = source.Name
            };
        }

    }
}
