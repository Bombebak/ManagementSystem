using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class FileController : Controller, IFileController
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileValidationService _fileValidationService;

        public FileController(ILogger<FileController> logger, IFileService fileService, ApplicationDbContext dbContext, IFileValidationService fileValidationService)
        {
            _logger = logger;
            _fileService = fileService;
            _dbContext = dbContext;
            _fileValidationService = fileValidationService;
        }

        public List<ApplicationFile> SaveFilesToDb(ApplicationMessage messageToBeSaved, List<IFormFile> files, string userId)
        {
            var applicationFiles = new List<ApplicationFile>();
            var applicationMessageFiles = new List<ApplicationMessageFile>();
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileValidation = _fileValidationService.Validate(ms.ToArray());
                        if (!fileValidation.IsValid)
                        {
                            continue;
                        }
                        // Upload the file if less than 2 MB
                        var fileToBeSaved = new ApplicationFile()
                        {
                            Content = ms.ToArray(),
                            CreationDate = DateTime.Now,
                            Name = file.FileName,
                            FileType = (int)fileValidation.FileType,
                            UserId = userId
                        };
                        applicationFiles.Add(fileToBeSaved);
                        applicationMessageFiles.Add(new ApplicationMessageFile()
                        {
                            File = fileToBeSaved,
                            Message = messageToBeSaved
                        });
                    }

                }
                _dbContext.Files.AddRange(applicationFiles);
                _dbContext.MessageFiles.AddRange(applicationMessageFiles);
            }
            return applicationFiles;
        }

        public List<ApplicationFile> SaveFilesToDb(ApplicationTask taskToBeSaved, List<IFormFile> files, string userId)
        {
            var applicationFiles = new List<ApplicationFile>();
            var applicationTaskFiles = new List<ApplicationTaskFile>();
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileValidation = _fileValidationService.Validate(ms.ToArray());
                        if (!fileValidation.IsValid)
                        {
                            continue;
                        }
                        // Upload the file if less than 2 MB
                        var fileToBeSaved = new ApplicationFile()
                        {
                            Content = ms.ToArray(),
                            CreationDate = DateTime.Now,
                            Name = file.FileName,
                            FileType = (int)fileValidation.FileType,
                            UserId = userId
                        };
                        applicationFiles.Add(fileToBeSaved);
                        applicationTaskFiles.Add(new ApplicationTaskFile()
                        {
                            File = fileToBeSaved,
                            Task = taskToBeSaved
                        });
                    }

                }
                _dbContext.Files.AddRange(applicationFiles);
                _dbContext.TaskFiles.AddRange(applicationTaskFiles);
            }
            return applicationFiles;
        }

        public List<FileUploadedViewModel> UpdateExistingFiles(ICollection<ApplicationMessageFile> messageFiles, List<FileUploadedViewModel> existingFiles)
        {
            if (messageFiles == null || !messageFiles.Any())
            {
                return existingFiles;
            }

            var messageFilesToBeDeleted = new List<ApplicationMessageFile>();
            foreach (var msgFile in messageFiles)
            {
                var exists = existingFiles.Any(e => e.Id == msgFile.FileId);
                if (!exists)
                {
                    messageFilesToBeDeleted.Add(msgFile);
                }
            }
            foreach (var msgFile in messageFilesToBeDeleted)
            {
                _dbContext.Files.Remove(msgFile.File);
                _dbContext.MessageFiles.Remove(msgFile);
            }
            return existingFiles;
        }

        public List<FileUploadedViewModel> UpdateExistingFiles(ICollection<ApplicationTaskFile> taskFiles, List<FileUploadedViewModel> existingFiles)
        {
            if (taskFiles == null || !taskFiles.Any())
            {
                return existingFiles;
            }

            var taskFilesToBeDeleted = new List<ApplicationTaskFile>();
            foreach (var taskFile in taskFiles)
            {
                var exists = existingFiles.Any(e => e.Id == taskFile.FileId);
                if (!exists)
                {
                    taskFilesToBeDeleted.Add(taskFile);
                }
            }
            foreach (var taskFile in taskFilesToBeDeleted)
            {
                _dbContext.Files.Remove(taskFile.File);
                _dbContext.TaskFiles.Remove(taskFile);
            }
            return existingFiles;
        }
    }
}
