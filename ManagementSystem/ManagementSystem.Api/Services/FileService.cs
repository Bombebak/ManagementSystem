using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _env;
        private readonly ILogger<FileService> _logger;

        private const string _userProfilePath = "\\images\\userprofiles\\";

        public FileService(IWebHostEnvironment env, ILogger<FileService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public string GetUserProfileImage(string userProfile)
        {
            if (string.IsNullOrEmpty(userProfile))
            {
                //TODO: Default image
                return string.Empty;
            }
            return _userProfilePath + userProfile;
        }

        public async Task<string> UploadUserProfile(string existingImagePath, IFormFile fileForm)
        {
            //TODO: Validate file content for malware
            //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
            var folder = Path.Combine(_env.WebRootPath, "images\\userprofiles");
            var fileName = Guid.NewGuid().ToString() + "_" + fileForm.FileName;
            var filePath = Path.Combine(folder, fileName);
            var uploadedResult = await AddImageAsync(filePath, fileForm);
            var deletedResult = DeleteImage(folder + "\\" + existingImagePath);
           
            return fileName;
        }

        public string GetFileIconClassByFileType(FileTypes fileType)
        {
            switch(fileType)
            {
                case FileTypes.Word:
                    return "fa fa-file-word-o";
                case FileTypes.Excel:
                    return "fa fa-file-excel-o";
                case FileTypes.PowerPoint:
                    return "fa fa-file-powerpoint-o";
                case FileTypes.Pdf:
                    return "fa fa-file-pdf-o";
                case FileTypes.Txt:
                    return "fa fa-file-text-o";
                case FileTypes.Png:
                case FileTypes.Jpg:
                case FileTypes.Jpeg:
                case FileTypes.Bmp:
                case FileTypes.Gif:
                    return "fa fa-picture-o";
                case FileTypes.Zip:
                    return "fa fa-file-zip-o";
                case FileTypes.Html:
                    return "fa fa-file-code-o";

            }
            return string.Empty;
        }

        private async Task<bool> AddImageAsync(string filePath, IFormFile fileForm)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await fileForm.CopyToAsync(fs);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception occured while trying to add image to server. Attempted path : " + filePath, ex);
                }                
            }
            return false;
        }

        private bool DeleteImage(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception occured while trying to delete image from server. Attempted path : " + filePath, ex);
                    }
                }
            }
            return false;
        }
    }
}
