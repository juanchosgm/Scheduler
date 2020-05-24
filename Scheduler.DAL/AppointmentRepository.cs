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

        public IQueryable<Appointment> GetAppointmentsByUser(int userID)
        {
            var appointments = context.Appointments.AsNoTracking().Include(a => a.User)
                .Where(a => a.User.UserID == userID);
            return appointments;
        }
    }
}
