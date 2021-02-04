using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Helpers;
using ManagementSystem.Api.Models.ViewModels.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class UserController : Controller, IUserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IUserService userService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableUsersInTeam(long? teamId)
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
                    usersInTeam = await _userRepository.GetUsersByTeamId(teamId.Value);
                }
                var users = await _userRepository.GetAllAsync();
                result.Data.AddRange(_userService.GetAvailableUsers(users, usersInTeam));
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to GetAvailableUsersInTeam for TeamId: " + teamId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableUsersInTask(long? taskId)
        {
            var result = new WebApiResult<List<ListItemDto<string>>>()
            {
                Data = new List<ListItemDto<string>>()
            };
            var usersInTask = new List<ApplicationUser>();
            try
            {
                if (taskId.HasValue)
                {
                    usersInTask = await _userRepository.GetUsersByTaskId(taskId.Value);
                }
                var users = await _userRepository.GetAllAsync();
                result.Data.AddRange(_userService.GetAvailableUsers(users, usersInTask));
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to GetAvailableUsersInTask for TaskId: " + taskId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }
    }
}
