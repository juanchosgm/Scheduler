using Scheduler.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserCategoryType UserCategoryType { get; set; }

        public virtual ICollection<Appointment> Appoiments { get; set; }
    }
}
