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

        public async Task<User> CreateUser(User user)
        {
            var newUser = context.Users.Add(user);
            await context.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User> GetUserByUsername(string email)
        {
            var users = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return users;
        }
    }
}
