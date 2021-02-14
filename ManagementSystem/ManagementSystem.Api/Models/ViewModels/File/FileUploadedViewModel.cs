using ManagementSystem.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.File
{
    public class FileUploadedViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public FileTypes FileType { get; set; }
        public string FileTypeNameForIcon { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
