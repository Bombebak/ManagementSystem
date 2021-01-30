using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Projects
{
    [ViewComponent(Name = "ProjectSave")]
    public class ProjectSaveViewComponent : ViewComponent
    {
        private readonly ILogger<ProjectSaveViewComponent> _logger;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMapping _projectMapping;

        public ProjectSaveViewComponent(ILogger<ProjectSaveViewComponent> logger, IProjectRepository projectRepository, IProjectMapping projectMapping)
        {
            _logger = logger;
            _projectRepository = projectRepository;
            _projectMapping = projectMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? taskId)
        {
            var data = new SaveProjectRequestViewModel();
            if (taskId.HasValue)
            {
                var entity = await _projectRepository.GetByIdAsync(taskId.Value);
                data = _projectMapping.MapViewModelFromDatalayer(entity);
            }

            return View("_ProjectSave", data);
        }
    }
}
