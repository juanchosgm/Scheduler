using Scheduler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DAL
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        IQueryable<Appointment> GetAppointmentsByUser(int userID);
    }
}
