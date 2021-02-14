using ManagementSystem.Api.Models.ViewModels.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Message
{
    public class MessageSaveRequestViewModel
    {
        public long? Id { get; set; }
        public long? TaskId { get; set; }
        public long? ParentId { get; set; }
        [Required(ErrorMessage = "Please enter a message")]
        public string Message { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<FileUploadedViewModel> ExistingFiles { get; set; }
        public bool HasLoaded { get; set; } //Used to check if data has to be loaded from id

        public List<int> Test { get; set; }

        public MessageSaveRequestViewModel()
        {
            Files = new List<IFormFile>();
            ExistingFiles = new List<FileUploadedViewModel>();
        }
    }
}
