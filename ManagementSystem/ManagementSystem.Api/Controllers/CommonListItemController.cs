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

        public List<ListItemDto<long>> GetProjects()
        {
            var list = new List<ListItemDto<long>>();
            try
            {
                var entries = _projectRepository.GetAll();
                list.AddRange(_listItemMapping.MapDtos(entries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to get and map projects to ListItemDtos");
            }

            return list;
        }

        public List<ListItemDto<long>> GetSprints()
        {
            var list = new List<ListItemDto<long>>();
            try
            {
                var entries = _sprintRepository.GetAll();
                list.AddRange(_listItemMapping.MapDtos(entries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to get and map projects to ListItemDtos");
            }

            return list;
        }
    }
}
