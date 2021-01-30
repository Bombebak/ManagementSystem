using ManagementSystem.Api.Models.ViewModels.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IModelStateService
    {
        List<ValidationItem> ValidateRequest(ModelStateDictionary modelState);
    }
}
