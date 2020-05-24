using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL;
using Scheduler.Models;
using Scheduler.Presentation.ViewModel;

namespace Scheduler.Presentation.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly Appointments appointments;

        public ScheduleController(Appointments appointments)
        {
            this.appointments = appointments;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Schedule";
            var initialModel = new ScheduleViewModel
            {
                ActionName = "AGREGAR CITAS",
                Action = "Index",
                Controller = "Schedule",
                ButtonTitle = "Agregar"
            };
            return View(initialModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ScheduleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appointment = new Appointment
                    {
                        Date = model.ScheduleDate.Value,
                        Description = model.Description,
                    };
                    await appointments.CreateAppointment(appointment);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}