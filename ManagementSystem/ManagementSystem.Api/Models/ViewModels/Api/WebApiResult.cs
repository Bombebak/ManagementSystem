using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Api
{
    public class WebApiResult<t>
    {
        public t Data { get; set; }
        public bool Success { get; set; }
        public List<ValidationItem> Items { get; set; }
        public List<ValidationItem> Messages { get; set; }

        public WebApiResult()
        {
            Items = new List<ValidationItem>();
            Messages = new List<ValidationItem>();
        }
    }
}
