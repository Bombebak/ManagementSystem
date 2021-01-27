using ManagementSystem.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Api
{
    public class ValidationItem
    {
        public string Message { get; set; }
        public ValidationTypes ValidationType { get; set; }
    }
}
