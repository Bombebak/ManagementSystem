using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Enums;
using ManagementSystem.Api.Models.ViewModels.File;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class FileMapping : IFileMapping
    {
        private readonly ILogger<FileMapping> _logger;
        private readonly IFileService _fileService;

        public FileMapping(ILogger<FileMapping> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public List<FileUploadedViewModel> MapFileUploadedToList(ICollection<ApplicationMessageFile> source)
        {
            var target = new List<FileUploadedViewModel>();

            if (source == null)
            {
                return target;
            }

            target.AddRange(source.Select(e => MapFileTuploadedItem(e)));

            return target;
        }

        private FileUploadedViewModel MapFileTuploadedItem(ApplicationMessageFile source)
        {
            var target = new FileUploadedViewModel();

            try
            {
                target.Id = source.FileId.Value;
                target.Name = source.File.Name;
                target.CreationDate = source.File.CreationDate;
                target.FileType = (FileTypes)source.File.FileType;
                target.FileTypeNameForIcon = _fileService.GetFileIconClassByFileType((FileTypes)source.File.FileType);
                target.Content = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(source.File.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to map MessageFileId: " + source?.Id);
            }

            return target;
        }
    }
}
