using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public byte[] File { get; set; }

        public virtual User User { get; set; }
    }
}
