using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HonestProject.DataModels
{
    public class HonestProjectContext : DbContext
    {
        public HonestProjectContext(DbContextOptions<HonestProjectContext> options)
            : base(options)
        {
        }

        public DbSet<HonestProject.DataModels.User> User { get; set; }
        public DbSet<HonestProject.DataModels.Role> Role { get; set; }
        public DbSet<HonestProject.DataModels.Site> Site { get; set; }
        public DbSet<HonestProject.DataModels.Team> Team { get; set; }
        public DbSet<HonestProject.DataModels.TimeEntry> TimeEntry { get; set; }
        public DbSet<HonestProject.DataModels.WorkType> WorkType { get; set; }
        public DbSet<HonestProject.DataModels.Project> Project { get; set; }
        public DbSet<HonestProject.DataModels.ProjectWorkType> ProjectWorkType { get; set; }
        public DbSet<HonestProject.DataModels.ProjectTemplate> ProjectTemplate { get; set; }
        public DbSet<HonestProject.DataModels.ProjectTemplateWorkType> ProjectTemplateWorkType { get; set; }
        public DbSet<HonestProject.DataModels.TimePercentageUserProjectWorkType> TimePercentageUserProjectWorkType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                            .HasOne(p => p.Team)
                            .WithMany(b => b.TeamMembers);

            modelBuilder.Entity<Team>()
            .HasOne(t => t.TeamLeader)
            .WithMany();

            modelBuilder.Entity<Team>()
            .HasOne(t => t.TeamManager)
            .WithMany();

            modelBuilder.Entity<TimePercentageUserProjectWorkType>()
            .HasKey(x => x.ID);

            modelBuilder.Entity<TimePercentageUserProjectWorkType>()
            .HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}