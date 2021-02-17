using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IFileController
    {
        List<ApplicationFile> SaveFilesToDb(ApplicationMessage messageToBeSaved, List<IFormFile> files, string userId);
        List<ApplicationFile> SaveFilesToDb(ApplicationTask taskToBeSaved, List<IFormFile> files, string userId);
        List<FileUploadedViewModel> UpdateExistingFiles(ICollection<ApplicationMessageFile> messageFiles, List<FileUploadedViewModel> existingFiles);
        List<FileUploadedViewModel> UpdateExistingFiles(ICollection<ApplicationTaskFile> taskFiles, List<FileUploadedViewModel> existingFiles);
    }
}
