using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Task
{
    public class TaskListCriteriaViewModel
    {
        public string SearchText { get; set; }
        [Display(Name = "Project")]
        public long? ProjectId { get; set; }
        [Display(Name = "Sprint")]
        public long? SprintId { get; set; }
        public List<string> UserEmails { get; set; }

        public List<Helpers.ListItemDto<long>> Projects { get; set; }
        public List<Helpers.ListItemDto<long>> Sprints { get; set; }


        public TaskListCriteriaViewModel()
        {
            InitializeList();
        }

        public TaskListCriteriaViewModel(string searchText, long? projectId, long? sprintId)
        {
            SearchText = searchText;
            ProjectId = projectId;
            SprintId = sprintId;
            InitializeList();
        }

        private void InitializeList()
        {
            Projects = new List<Helpers.ListItemDto<long>>();
            Sprints = new List<Helpers.ListItemDto<long>>();
            UserEmails = new List<string>();
        }
    }
}
