using Microsoft.AspNetCore.Identity;
using Scheduler.DAL;
using Scheduler.Models;
using System;
using System.Threading.Tasks;

namespace Scheduler.BL
{
    public class Users
    {
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        private readonly IUserRepository userRepository;

        public Users(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await userRepository.GetUserByUsername(email);
            if (user != null)
            {
                return (passwordHasher.VerifyHashedPassword(user, user.Password, password)) switch
                {
                    PasswordVerificationResult.Success => true,
                    PasswordVerificationResult.Failed => false,
                    _ => false,
                };
            }
            else
            {
                return false;
            }
        }

        public async Task CreateUser(User user)
        {
            var userExistsing = await userRepository.GetUserByUsername(user.Email);
            if (userExistsing == null)
            {
                user.Password = passwordHasher.HashPassword(user, user.Password);
                await userRepository.CreateUser(user);
            }
            else
            {
                throw new Exception($"The email: {user.Email} already exists");
            }
        }
    }
}
