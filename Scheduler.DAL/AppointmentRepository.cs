using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DAL
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SchedulerDbContext context;

        public AppointmentRepository(SchedulerDbContext context)
        {
            this.context = context;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            var newAppointment = context.Appointments.Add(appointment);
            await context.SaveChangesAsync();
            return newAppointment.Entity;
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
        }

        public async Task<Appointment> EditAppointment(Appointment appointment)
        {
            var newAppointment = context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return newAppointment.Entity;
        }

        public async Task<Appointment> GetAppointmentByID(int id)
        {
            var appointment = await context.Appointments.Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AppointmentID == id);
            return appointment;
        }

        public IQueryable<Appointment> GetAppointmentsByUser(int userID)
        {
            var appointments = context.Appointments.AsNoTracking().Include(a => a.User)
                .Where(a => a.User.UserID == userID);
            return appointments;
        }
    }
}
