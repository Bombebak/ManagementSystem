﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Project
{
    public class SaveProjectRequestViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
