using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListItemViewModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<TaskListItemViewModel> Children { get; set; }
        public List<TaskListUserItemViewModel> Users { get; set; }

        public TaskListItemViewModel()
        {
            Children = new List<TaskListItemViewModel>();
            Users = new List<TaskListUserItemViewModel>();
        }
    }

    public class TaskListUserItemViewModel
    {
        public string Image { get; set; }
        public string Email { get; set; }
        public string ProfilePath { get; set; }

        public TaskListUserItemViewModel(string image, string email, string profilePath)
        {
            Image = image;
            Email = email;
            ProfilePath = profilePath;
        }
    }
}
