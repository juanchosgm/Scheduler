using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL;

namespace Scheduler.Presentation.Components
{
    [Authorize]
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
