using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DAL
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserByUsername(string email);
    }
}
