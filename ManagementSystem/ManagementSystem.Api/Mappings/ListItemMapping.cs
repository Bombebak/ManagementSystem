using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class ListItemMapping : IListItemMapping
    {
        private readonly ILogger<ListItemMapping> _logger;


        public List<ListItemDto<long>> MapDtos(IEnumerable<ApplicationProject> source)
        {
            var result = new List<ListItemDto<long>>();
            if (source == null)
            {
                return result;
            }

            try
            {
                result.AddRange(source.Select(e => new ListItemDto<long>(e.Id, e.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to map application projects to listItemDto's");
            }

            return result;
        }

        public List<ListItemDto<long>> MapDtos(IEnumerable<ApplicationSprint> source)
        {
            var result = new List<ListItemDto<long>>();
            if (source == null)
            {
                return result;
            }

            try
            {
                result.AddRange(source.Select(e => new ListItemDto<long>(e.Id, e.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to map application projects to listItemDto's");
            }

            return result;
        }
    }
}
