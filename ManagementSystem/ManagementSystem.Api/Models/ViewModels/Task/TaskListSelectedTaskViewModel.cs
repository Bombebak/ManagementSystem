using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListSelectedTaskViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public string Project { get; set; }
        public string Sprint { get; set; }
    }
}
