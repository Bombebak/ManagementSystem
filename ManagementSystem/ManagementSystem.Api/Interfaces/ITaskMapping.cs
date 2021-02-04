using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ITaskMapping
    {
        List<TaskListItemViewModel> MapToTaskList(IEnumerable<ApplicationTask> source);
        SaveTaskRequestViewModel MapToSaveTask(ApplicationTask source);
        void MapTaskToBeSaved(ApplicationTask target, SaveTaskRequestViewModel source);
        TaskListSelectedTaskViewModel MapToSelectedTask(ApplicationTask source);
    }
}
