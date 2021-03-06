﻿using ManagementSystem.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IFileMapping
    {
        List<Models.ViewModels.File.FileUploadedViewModel> MapFileUploadedToList(ICollection<ApplicationMessageFile> source);
        List<Models.ViewModels.File.FileUploadedViewModel> MapFileUploadedToList(ICollection<ApplicationTaskFile> source);
    }
}
