using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly SchedulerDbContext context;

        public UserRepository(SchedulerDbContext context)
        {
            this.context = context;
        }

        public async Task CreateUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsername(string email)
        {
            var users = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return users;
        }
    }
}
