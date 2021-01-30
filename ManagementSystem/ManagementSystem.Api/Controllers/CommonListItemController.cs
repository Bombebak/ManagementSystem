using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class CommonListItemController : Controller, ICommonListItemController
    {
        private readonly ILogger<CommonListItemController> _logger;
        private readonly ISprintRepository _sprintRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IListItemMapping _listItemMapping;

        public CommonListItemController(ILogger<CommonListItemController> logger, ISprintRepository sprintRepository, IProjectRepository projectRepository, IListItemMapping listItemMapping)
        {
            _logger = logger;
            _sprintRepository = sprintRepository;
            _projectRepository = projectRepository;
            _listItemMapping = listItemMapping;
        }

        public async Task<List<ListItemDto<long>>> GetProjectsAsync(bool includeDefault)
        {
            var list = GetStandardList(includeDefault);

            try
            {
                var entries = await _projectRepository.GetAllAsync();
                list.AddRange(_listItemMapping.MapDtos(entries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to get and map projects to ListItemDtos");
            }

            return list;
        }

        public async Task<List<ListItemDto<long>>> GetSprintsAsync(bool includeDefault)
        {
            var list = GetStandardList(includeDefault);
            try
            {
                var entries = await _sprintRepository.GetAllAsync();
                list.AddRange(_listItemMapping.MapDtos(entries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to get and map projects to ListItemDtos");
            }

            return list;
        }

        private List<ListItemDto<long>> GetStandardList(bool includeDefault)
        {
            var list = new List<ListItemDto<long>>();
            if (includeDefault)
            {
                list.Add(new ListItemDto<long>(0, "Vælg"));
            }
            return list;
        }
    }
}
