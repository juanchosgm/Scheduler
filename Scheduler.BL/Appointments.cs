using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Scheduler.DAL;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.BL
{
    public class Appointments
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IUserRepository userRepository;

        public Appointments(IHttpContextAccessor httpContext, IAppointmentRepository appointmentRepository,
            IUserRepository userRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.userRepository = userRepository;
            this.httpContext = httpContext;
        }

        private string EmailUserAuthenticated
        {
            get
            {
                var principal = httpContext.HttpContext.User;
                var claim = principal.FindFirst(ClaimTypes.Email);
                return claim.Value;
            }
        }

        private int IdUserAuthenticated
        {
            get
            {
                var principal = httpContext.HttpContext.User;
                var claim = principal.FindFirst(ClaimTypes.Sid);
                return int.Parse(claim.Value);
            }
        }

        public async Task CreateAppointment(Appointment appointment)
        {
            appointment.User = await userRepository.GetUserByUsername(EmailUserAuthenticated);
            var appointments = appointmentRepository.GetAppointmentsByUser(appointment.User.UserID)
                .Where(a => a.Date == appointment.Date);
            if (await appointments.AnyAsync())
            {
                throw new Exception("La fecha y la hora ya se encuentran ocupadas");
            }
            await appointmentRepository.CreateAppointment(appointment);
        }

        public async Task DeleteAppointment(int appointmentID)
        {
            var appointment = await appointmentRepository.GetAppointmentByID(appointmentID);
            if (appointment != null)
            {
                await appointmentRepository.DeleteAppointment(appointment);
            }
            else
            {
                throw new Exception("La cita no existe");
            }
        }

        public async Task EditAppointment(int appointmentID, Appointment appointment)
        {
            var appointmentToEdit = await appointmentRepository.GetAppointmentByID(appointmentID);
            if (appointmentToEdit != null)
            {
                appointmentToEdit.Date = appointment.Date;
                appointmentToEdit.Description = appointment.Description;
                appointmentToEdit.FileName = appointment.FileName;
                appointmentToEdit.FileDescription = appointment.FileDescription;
                appointmentToEdit.File = appointment.File;
                await appointmentRepository.EditAppointment(appointmentToEdit);
            }
            else
            {
                throw new Exception("La cita no exista");
            }
        }

        public async Task<Appointment> GetAppointmentByID(int appointmentID)
        {
            var appointment = await appointmentRepository.GetAppointmentByID(appointmentID);
            if (appointment != null)
            {
                return appointment;
            }
            else
            {
                throw new Exception("La cita no exista");
            }
        }

        public IAsyncEnumerable<Appointment> GetAppointmentUser()
        {
            var appointments = appointmentRepository.GetAppointmentsByUser(IdUserAuthenticated)
                .OrderBy(a => a.Date);
            return appointments.AsAsyncEnumerable();
        }
    }
}
