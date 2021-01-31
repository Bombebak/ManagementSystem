﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListViewModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public List<TaskListViewModel> Children { get; set; }
    }
}
