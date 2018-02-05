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
    }
}