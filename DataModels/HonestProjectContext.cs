using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HonestProject.DataModels
{
    public class HonestProjectContext : DbContext
    {
        public HonestProjectContext (DbContextOptions<HonestProjectContext> options)
            : base(options)
        {
        }

        public DbSet<HonestProject.DataModels.User> User { get; set; }
        public DbSet<HonestProject.DataModels.Role> Role { get; set; }
        public DbSet<HonestProject.DataModels.Site> Site { get; set; }
        public DbSet<HonestProject.DataModels.Team> Team { get; set; }
         public DbSet<HonestProject.DataModels.TimeEntry> TimeEntry { get; set; }
    }
}