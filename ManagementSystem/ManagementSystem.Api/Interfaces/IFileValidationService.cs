using ManagementSystem.Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IFileValidationService
    {
        FileValidationResultDto Validate(IFormFile file);
        FileValidationResultDto Validate(byte[] bytes);
    }
}
