using System;
using System.Linq;
using HonestProject.DataModels;

namespace HonestProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HonestProjectContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Site.Any())
            {
                return;   // DB has been seeded
            }

            Site site = new Site();
            site.Name = "Texas Legislative Council";
            site.IncludeWeekends = false;
            site.UniqueSiteId = "txlcis";
            site.HoursPerDay = 8;
            site.PublicIdentifier = Guid.NewGuid();
            context.Site.Add(site);
            context.SaveChanges();

            var roles = new Role[] {
                new Role() {PublicIdentifier = Guid.NewGuid(), Description = "Does all site administration. Has all privliges", Name = "Site Administrator"},
                new Role() {PublicIdentifier = Guid.NewGuid(), Description = "Manages teams", Name = "Manager"},
                new Role() {PublicIdentifier = Guid.NewGuid(), Description = "Leads teams", Name = "Team Leader"},
                new Role() {PublicIdentifier = Guid.NewGuid(), Description = "A member of a team", Name = "Team Member"}
            };

            foreach (Role s in roles)
            {
                context.Role.Add(s);
            }
            context.SaveChanges();

            Role teamMember = context.Role.Where(x => x.Name == "Team Member").FirstOrDefault();
            Role teamLeader = context.Role.Where(x => x.Name == "Team Leader").FirstOrDefault();
            Role manager = context.Role.Where(x => x.Name == "Manager").FirstOrDefault();
            Role siteAdministrator = context.Role.Where(x => x.Name == "Site Administrator").FirstOrDefault();

            var users = new User[]{
                new User() { Site = site, FirstName = "Colin", LastName = "Gormley", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "colin@b.c", PublicIdentifier = Guid.NewGuid(), Role = siteAdministrator },
                new User() { Site = site, FirstName = "Eric", LastName = "Lavangi", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "eric@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Osama", LastName = "Abdullahussein", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "osama@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Kevin", LastName = "Welcht", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "kevin@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Rebecca", LastName = "Garcia", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "rebecca@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamLeader },
                new User() { Site = site, FirstName = "Kris", LastName = "Doer", CreatedDate = DateTime.Now, PasswordHash = "fakepassword", EmailAddress = "kris@b.c", PublicIdentifier = Guid.NewGuid(), Role = manager }
                
            };

            foreach (User s in users)
            {
                context.User.Add(s);
            }
            context.SaveChanges();

            User leader = context.User.Where(x => x.EmailAddress == "rebecca@b.c").FirstOrDefault();
            User userManager = context.User.Where (x => x.EmailAddress == "kris@b.c").FirstOrDefault();

            var team = new Team {Site = site, Name = "Picante", PublicIdentifier = Guid.NewGuid(), TeamLeader = leader, TeamManager = userManager, Description = "Responsible for everything important",TeamMembers = new System.Collections.Generic.List<User>()};
            User kevin = context.User.Where(x => x.EmailAddress == "kevin@b.c").FirstOrDefault();
            User osama = context.User.Where(x => x.EmailAddress == "osama@b.c").FirstOrDefault();
            team.TeamMembers.Add(leader);
            team.TeamMembers.Add(kevin);
            team.TeamMembers.Add(osama);
            context.Team.Add(team);
            context.SaveChanges();

            

        }
    }
}