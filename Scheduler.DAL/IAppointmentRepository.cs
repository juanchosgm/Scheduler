using Scheduler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DAL
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task DeleteAppointment(Appointment appointment);
        Task<Appointment> EditAppointment(Appointment appointment);
        Task<Appointment> GetAppointmentByID(int id);
        IQueryable<Appointment> GetAppointmentsByUser(int userID);
    }
}
