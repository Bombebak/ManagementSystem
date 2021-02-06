using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IFileService
    {
        string GetUserProfileImage(string userProfile);
        Task<string> UploadUserProfile(string existingImagePath, IFormFile fileForm);

    }
}
