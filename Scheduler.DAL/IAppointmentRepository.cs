using Scheduler.Models;
using System.Linq;

namespace Scheduler.DAL
{
    public interface IAppointmentRepository
    {
        IQueryable<Appointment> GetAppointmentsByUser(int userID);
    }
}
