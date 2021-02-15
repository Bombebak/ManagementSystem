using ManagementSystem.Api.Models.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Message
{
    public class MessageListViewModel
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public MessageAuthorViewModel Author { get; set; }
        public string CreationDateStr { get; set; }
        public string Message { get; set; }
        public int Level { get; set; }
        public List<FileUploadedViewModel> Files { get; set; }
        public string CurrentUserId { get; set; }
    }

    public class MessageAuthorViewModel
    {
        public string Id { get; set; }
        public string ProfileImagePath { get; set; }
        public string Name { get; set; }
    }
}
