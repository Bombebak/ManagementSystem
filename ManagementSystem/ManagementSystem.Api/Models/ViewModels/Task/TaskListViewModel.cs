using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListViewModel
    {
        public TaskListCriteriaViewModel Criteria { get; set; }

        public TaskListViewModel()
        {
        }
    }
}
