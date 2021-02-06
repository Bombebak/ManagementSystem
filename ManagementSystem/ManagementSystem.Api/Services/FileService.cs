using ManagementSystem.Api.Interfaces;
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
                return string.Empty;
            }
            return _userProfilePath + userProfile;
        }

        public async Task<string> UploadUserProfile(string existingImagePath, IFormFile fileForm)
        {
            //TODO: Validate file content for malware
            var folder = Path.Combine(_env.WebRootPath, "images\\userprofiles");
            var fileName = Guid.NewGuid().ToString() + "_" + fileForm.FileName;
            var filePath = Path.Combine(folder, fileName);
            var uploadedResult = await AddImageAsync(filePath, fileForm);
            var deletedResult = DeleteImage(folder + "\\" + existingImagePath);
           
            return fileName;
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
