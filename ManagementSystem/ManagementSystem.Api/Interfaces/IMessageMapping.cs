using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IMessageMapping
    {
        MessageSaveRequestViewModel MapToViewModelFromDatalayer(ApplicationMessage source);
        List<MessageListViewModel> MapToListFromDatalayer(IEnumerable<ApplicationMessage> source, string currentUserId);
    }
}
