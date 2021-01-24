using ManagementSystem.Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IAccountController
    {
        Task<IActionResult> Register(RegisterViewModel input);
        Task<IActionResult> Login(LoginViewModel input);
    }
}
