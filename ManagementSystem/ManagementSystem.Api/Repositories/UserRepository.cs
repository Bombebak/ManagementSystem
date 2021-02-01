using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<List<ApplicationUser>> GetAllAsync()
        {
            return _dbContext.Users.ToListAsync();
        }

        public Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
