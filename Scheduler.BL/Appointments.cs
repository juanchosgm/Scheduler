using Microsoft.EntityFrameworkCore;
using Scheduler.DAL;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.BL
{
    public class Appointments
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IUserRepository userRepository;

        public Appointments(IAppointmentRepository appointmentRepository, IUserRepository userRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateAppointment(Appointment appointment)
        {
            appointment.User = await userRepository.GetUserByUsername("juanchosgm@hotmail.com");
            var appointments = appointmentRepository.GetAppointmentsByUser(appointment.User.UserID)
                .Where(a => a.Date == appointment.Date);
            if (await appointments.AnyAsync())
            {
                throw new Exception("La fecha y la hora ya se encuentran ocupadas");
            }
            await appointmentRepository.CreateAppointment(appointment);
        }

        public IAsyncEnumerable<Appointment> GetAppointmentUser()
        {
            var appointments = appointmentRepository.GetAppointmentsByUser(1);
            return appointments.AsAsyncEnumerable();
        }
    }
}
