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
    [ViewComponent(Name = "ProjectList")]
    public class ProjectListViewComponent : ViewComponent
    {
        private readonly ILogger<ProjectListViewComponent> _logger;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMapping _projectMapping;

        public ProjectListViewComponent(ILogger<ProjectListViewComponent> logger, IProjectRepository projectRepository, IProjectMapping projectMapping)
        {
            _logger = logger;
            _projectRepository = projectRepository;
            _projectMapping = projectMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<ProjectListViewModel>();

            try
            {
                var tasks = await _projectRepository.GetAllAsync();
                data = _projectMapping.MapToProjectList(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load tasks for userId: ");
            }

            return View("_ProjectList", data);
        }
    }
}
