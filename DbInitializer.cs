using System;
using System.Collections.Generic;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.Utilities;

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

            PasswordHashUtility utility = new PasswordHashUtility();

            var users = new User[]{
                new User() { Site = site, FirstName = "Colin", LastName = "Gormley", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "colin@b.c", PublicIdentifier = Guid.NewGuid(), Role = siteAdministrator },
                new User() { Site = site, FirstName = "Eric", LastName = "Lavangi", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "eric@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Osama", LastName = "Abdullahussein", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "osama@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Kevin", LastName = "Welcht", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "kevin@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Garrett", LastName = "Thomas", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "garrett@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamMember },
                new User() { Site = site, FirstName = "Rebecca", LastName = "Garcia", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "rebecca@b.c", PublicIdentifier = Guid.NewGuid(), Role = teamLeader },
                new User() { Site = site, FirstName = "Kris", LastName = "Doer", CreatedDate = DateTime.Now, PasswordHash = utility.CalculateHash("fakepassword"), EmailAddress = "kris@b.c", PublicIdentifier = Guid.NewGuid(), Role = manager }

            };

            foreach (User s in users)
            {
                context.User.Add(s);
            }
            context.SaveChanges();

            User leader = context.User.Where(x => x.EmailAddress == "rebecca@b.c").FirstOrDefault();
            User userManager = context.User.Where(x => x.EmailAddress == "kris@b.c").FirstOrDefault();

            var team = new Team { Site = site, Name = "Picante", PublicIdentifier = Guid.NewGuid(), TeamLeader = leader, TeamManager = userManager, Description = "Responsible for everything important", TeamMembers = new System.Collections.Generic.List<User>() };
            User kevin = context.User.Where(x => x.EmailAddress == "kevin@b.c").FirstOrDefault();
            User osama = context.User.Where(x => x.EmailAddress == "osama@b.c").FirstOrDefault();
            team.TeamMembers.Add(leader);
            team.TeamMembers.Add(kevin);
            team.TeamMembers.Add(osama);
            team.TeamMembers.Add(users[0]);
            team.TeamMembers.Add(users[1]);
            context.Team.Add(team);
            context.SaveChanges();

            var workTypes = new WorkType[]{
                new WorkType() {Name = "Planning",  PublicIdentifier = Guid.NewGuid()},
                new WorkType() {Name = "Development",  PublicIdentifier = Guid.NewGuid()},
                new WorkType() {Name = "Testing",  PublicIdentifier = Guid.NewGuid()},
                new WorkType() {Name = "Deployment",  PublicIdentifier = Guid.NewGuid()}
            };

            foreach (WorkType type in workTypes)
            {
                context.WorkType.Add(type);
            }
            context.SaveChanges();

            ProjectTemplate template = new ProjectTemplate();
            template.Name = "New Application";
            template.WorkTypes = new List<ProjectTemplateWorkType>();
            template.PublicIdentifier = Guid.NewGuid();
            foreach (var type in workTypes)
            {
                ProjectTemplateWorkType projType = new ProjectTemplateWorkType();
                projType.WorkType = type;
                projType.PublicIdentifier = Guid.NewGuid();
                template.WorkTypes.Add(projType);
            }

            context.ProjectTemplate.Add(template);
            context.SaveChanges();

            Project project = new Project();
            project.Color = "green";
            project.Name = "Adv. Search Web";
            project.OwningTeam = team;
            project.PercentageEstimate = 50;
            project.PublicIdentifier = Guid.NewGuid();
            project.StartDate = DateTime.Parse("4/1/2018");
            project.WorkTypeItems = new List<ProjectWorkType>();

            var projectItems = new ProjectWorkType[] {
                new ProjectWorkType() {WorkType = workTypes[0], ManHours = 300, PublicIdentifier = Guid.NewGuid()},
                new ProjectWorkType() {WorkType = workTypes[1], ManHours = 2880, PublicIdentifier = Guid.NewGuid()},
                new ProjectWorkType() {WorkType = workTypes[2], ManHours = 1440, PublicIdentifier = Guid.NewGuid()},
                new ProjectWorkType() {WorkType = workTypes[3], ManHours = 60, PublicIdentifier = Guid.NewGuid()}
            };

            foreach (var type in projectItems)
            {
                project.WorkTypeItems.Add(type);
            }

            context.Project.Add(project);
            context.SaveChanges();

/* 
            var timePct = new TimePercentageUserProjectWorkType[] {
                //Planning
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[0], TeamMember = users[5], WorkPercentage = 75},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[0], TeamMember = users[4], WorkPercentage = 25},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[0], TeamMember = users[0], WorkPercentage = 15},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[0], TeamMember = users[1], WorkPercentage = 15},

                //Development
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[0], WorkPercentage = 75},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[1], WorkPercentage = 75},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[2], WorkPercentage = 95},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[3], WorkPercentage = 95},

                //Testing
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[2], TeamMember = users[5], WorkPercentage = 25},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[2], TeamMember = users[4], WorkPercentage = 75},

                //Deployment
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[0], WorkPercentage = 10},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[1], WorkPercentage = 10},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[2], WorkPercentage = 5},
                new TimePercentageUserProjectWorkType() {ProjectWorkType = projectItems[1], TeamMember = users[3], WorkPercentage = 5},
            };

            foreach (var time in timePct)
            {
                context.TimePercentageUserProjectWorkType.Add(time);
            }
            */

            context.SaveChanges();

        }
    }
}