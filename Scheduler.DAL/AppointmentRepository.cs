using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System.Linq;

namespace Scheduler.DAL
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SchedulerDbContext context;

        public AppointmentRepository(SchedulerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Appointment> GetAppointmentsByUser(int userID)
        {
            var appointments = context.Appointments.AsNoTracking().Include(a => a.User)
                .Where(a => a.User.UserID == userID);
            return appointments;
        }
    }
}
