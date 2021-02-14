using ManagementSystem.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.Dto
{
    public class FileValidationResultDto
    {
        public bool IsValid { get; set; }
        public FileTypes FileType { get; set; }
    }
}
