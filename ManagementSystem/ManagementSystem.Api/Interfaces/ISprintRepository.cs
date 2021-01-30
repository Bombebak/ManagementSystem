using ManagementSystem.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ISprintRepository
    {
        Task<ApplicationSprint> GetByIdAsync(long id);
        Task<List<ApplicationSprint>> GetAllAsync();
    }
}
