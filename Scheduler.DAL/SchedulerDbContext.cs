using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.DAL
{
    public class SchedulerDbContext : DbContext
    {
        public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
