﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListSelectedTaskViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public long SprintId { get; set; }
        public long ProjectId { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public List<Helpers.ListItemDto<long>> Projects { get; set; }
        public List<Helpers.ListItemDto<long>> Sprints { get; set; }
    }
}