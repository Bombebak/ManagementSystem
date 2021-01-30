using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Sprint
{
    public class SaveSprintRequestViewModel
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please select end date")]
        public DateTime EndDate { get; set; }
    }
}
