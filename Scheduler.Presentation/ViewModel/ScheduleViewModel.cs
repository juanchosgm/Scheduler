using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Presentation.ViewModel
{
    public class ScheduleViewModel
    {
        public string ActionName { get; set; }
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public DateTime? ScheduleDate { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string ButtonTitle { get; set; }
    }
}
