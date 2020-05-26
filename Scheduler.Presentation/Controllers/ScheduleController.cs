using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL;
using Scheduler.Models;
using Scheduler.Presentation.ViewModel;

namespace Scheduler.Presentation.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly Appointments appointments;

        public ScheduleController(Appointments appointments)
        {
            ViewBag.Title = "Schedule";
            this.appointments = appointments;
        }

        public IActionResult Index()
        {
            var initialModel = new ScheduleViewModel
            {
                ActionName = "AGREGAR CITAS",
                Action = nameof(CreateAppointment),
                Controller = "Schedule",
                ButtonTitle = "Agregar"
            };
            return View(initialModel);
        }

        public async Task<IActionResult> EditAppointment(int appointmentID)
        {
            try
            {
                var appointment = await appointments.GetAppointmentByID(appointmentID);
                var initialModel = new ScheduleViewModel
                {
                    ActionName = "EDITAR CITA",
                    Action = nameof(EditAppointment),
                    Controller = "Schedule",
                    ScheduleDate = appointment.Date,
                    Description = appointment.Description,
                    Id = appointment.AppointmentID,
                    ButtonTitle = "Editar"
                };
                return View("Index", initialModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(ScheduleViewModel model)
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
                    if (model.File != null)
                    {
                        appointment.FileName = model.File.FileName;
                        appointment.FileDescription = model.File.ContentType;
                        using var memoryStream = new MemoryStream();
                        await model.File.CopyToAsync(memoryStream);
                        appointment.File = memoryStream.ToArray();
                    }
                    await appointments.CreateAppointment(appointment);
                    return RedirectToAction("Index");
                }
                model.ActionName = "AGREGAR CITAS";
                model.ButtonTitle = "Agregar";
                return View("Index", model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(int appointmentID, ScheduleViewModel model)
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
                    if (model.File != null)
                    {
                        appointment.FileName = model.File.FileName;
                        appointment.FileDescription = model.File.ContentType;
                        using var memoryStream = new MemoryStream();
                        await model.File.CopyToAsync(memoryStream);
                        appointment.File = memoryStream.ToArray();
                    }
                    await appointments.EditAppointment(appointmentID, appointment);
                    return RedirectToAction("Index");
                }
                model.ActionName = "EDITAR CITA";
                model.ButtonTitle = "Editar";
                return View("Index", model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteAppoinment(int appointmentID)
        {
            try
            {
                await appointments.DeleteAppointment(appointmentID);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DownloadFile(int appointmentID)
        {
            try
            {
                var appointment = await appointments.GetAppointmentByID(appointmentID);
                return File(appointment.File, appointment.FileDescription, appointment.FileName);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}