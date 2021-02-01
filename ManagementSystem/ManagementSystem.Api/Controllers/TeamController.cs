using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Helpers;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class TeamController : Controller, ITeamController
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMapping _teamMapping;
        private readonly ApplicationDbContext _dbContext;
        private readonly IModelStateService _modelStateService;
        private readonly IUserRepository _userRepository;
        private readonly ITeamUserService _teamUserService;

        public TeamController(ILogger<TeamController> logger, ITeamRepository teamRepository, ITeamMapping teamMapping, ApplicationDbContext dbContext, 
            IModelStateService modelStateService, IUserRepository userRepository, ITeamUserService teamUserService)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _teamMapping = teamMapping;
            _dbContext = dbContext;
            _modelStateService = modelStateService;
            _userRepository = userRepository;
            _teamUserService = teamUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SaveTeam(long? teamId, long? teamParentId)
        {
            return ViewComponent("TeamSave", new { teamId, teamParentId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTeamUsers(long? teamId)
        {
            var result = new WebApiResult<List<ListItemDto<string>>>()
            {
                Data = new List<ListItemDto<string>>()
            };
            var usersInTeam = new List<ApplicationUser>();
            try
            {
                if (teamId.HasValue)
                {
                    usersInTeam = await _teamRepository.GetUsersInTeam(teamId.Value);
                }
                var users = await _userRepository.GetAllAsync();
                result.Data.AddRange(_teamUserService.GetAvailableUsers(users, usersInTeam));
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to GetAvailableTeamUsers for TeamId: " + teamId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }            

            return Json(new { result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTeam(TeamSaveRequestViewModel request)
        {
            var result = new WebApiResult<TeamListViewModel>();

            result.Items = _modelStateService.ValidateRequest(ModelState);
            if (result.Items.Any())
            {
                return Json(new { result });
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                result.Success = await SaveItem(request, userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Very nice",
                    ValidationType = Models.Enums.ValidationTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to save taskId: " + request.Id + " for userId: " + userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        private async Task<bool> SaveItem(TeamSaveRequestViewModel request, string userId)
        {
            Data.Entities.ApplicationTeam teamToBeSaved = new Data.Entities.ApplicationTeam();
            if (request.Id.GetValueOrDefault() != 0)
            {
                teamToBeSaved = await _teamRepository.GetByIdAsync(request.Id.Value);
            }
            else
            {
                _dbContext.Add(teamToBeSaved);
            }

            teamToBeSaved = _teamMapping.MapDatalayerFromViewModel(teamToBeSaved, request);
            await _dbContext.SaveChangesAsync();

            var updated = await UpdateTeamUsers(teamToBeSaved, request.UserEmailsInTeam);

            if (updated)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> UpdateTeamUsers(ApplicationTeam teamToBeSaved, List<string> savedTeamUserEmails)
        {          
            DeleteExistingTeamUsers(teamToBeSaved, savedTeamUserEmails);
            await AddNewTeamUsers(teamToBeSaved, savedTeamUserEmails);
            var result = await _dbContext.SaveChangesAsync();
            return result == 1;
        }

        private void DeleteExistingTeamUsers(ApplicationTeam teamToBeSaved, List<string> savedTeamUserEmails)
        {
            var teamUsersToDelete = new List<ApplicationTeamUser>();

            var existingTeamUsers = teamToBeSaved.TeamUsers;
            if (existingTeamUsers != null)
            {
                foreach (var teamUser in existingTeamUsers)
                {
                    if (savedTeamUserEmails.FirstOrDefault(e => e == teamUser.User.Email) == null)
                    {
                        teamUsersToDelete.Add(teamUser);
                    }
                }
            }
            _dbContext.TeamUsers.RemoveRange(teamUsersToDelete);
        }

        private async Task<bool> AddNewTeamUsers(ApplicationTeam teamToBeSaved, List<string> savedTeamUserEmails)
        {
            var teamUsersToAdd = new List<ApplicationTeamUser>();

            var existingUsersInTeam = teamToBeSaved.TeamUsers?.Select(e => e.User);

            foreach (var email in savedTeamUserEmails)
            {
                if (existingUsersInTeam?.FirstOrDefault(e => e.Email == email) == null)
                {
                    var teamUserToAdd =new ApplicationTeamUser()
                    {
                        User = await _userRepository.GetUserByEmailAsync(email),
                        Team = teamToBeSaved
                    };
                    _dbContext.TeamUsers.Add(teamUserToAdd);
                }
            }

            return true;
        }
    }
}
