using Microsoft.AspNetCore.Mvc;
using Scheduler.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Presentation.Components
{
    public class AppointmentList : ViewComponent
    {
        private readonly Appointments appointments;

        public AppointmentList(Appointments appointments)
        {
            this.appointments = appointments;
        }

        public IViewComponentResult Invoke()
        {
            var appointments = this.appointments.GetAppointmentUser();
            return View(appointments);
        }
    }
}
