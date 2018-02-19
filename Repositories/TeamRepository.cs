using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Repositories
{

    public class TeamRepository : BasicRepository, ITeamRepository
    {
        HonestProjectContext context;
        IConfiguration configuration;

        public TeamRepository(HonestProjectContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public ViewModels.Team[] GetManagedTeams(string userName)
        {
            DataModels.User requestingUser = this.context.User.Where(x => x.EmailAddress == userName).FirstOrDefault();
            DataModels.Team[] teams = this.context.Team.Include(x => x.TeamManager)
            .Include(x => x.TeamMembers).ThenInclude(x => x.Role)
            .Where(x => x.TeamManager == requestingUser).ToArray();
            List<ViewModels.Team> viewTeams = new List<ViewModels.Team>();

            foreach (DataModels.Team team in teams)
            {
                ViewModels.Team viewTeam = new ViewModels.Team();
                viewTeam.Name = team.Name;
                viewTeam.Description = team.Description;
                viewTeam.ID = team.PublicIdentifier;
                List<TeamMember> members = new List<TeamMember>();
                foreach (DataModels.User user in team.TeamMembers)
                {
                    TeamMember member = new TeamMember();
                    member.Name = user.LastName + ", " + user.FirstName;
                    member.PublicIdentifier = user.PublicIdentifier;
                    member.Role = user.Role.Name;
                    members.Add(member);
                }
                viewTeam.TeamMembers = members.ToArray();
                viewTeams.Add(viewTeam);
            }

            return viewTeams.ToArray();
        }

        public ViewModels.Team Save(ViewModels.RegisterTeam newTeam, string userName)
        {
            if (!ValidateTeam(newTeam, userName))
            {
                this.ValidationFailed();
                return null;
            }

            this.ValidationPassed();
            DataModels.Team dbTeam = new DataModels.Team();
            dbTeam.Name = newTeam.Name;
            dbTeam.PublicIdentifier = Guid.NewGuid();
            DataModels.User postingUser = this.context.User.Where(x => x.EmailAddress == userName).FirstOrDefault();
            dbTeam.Site = postingUser.Site;
            dbTeam.Description = newTeam.Description;
            if (newTeam.TeamLeaderId != null)
            {
                DataModels.User teamLeader = this.context.User.Include(x => x.Role).Where(x => x.PublicIdentifier == newTeam.TeamLeaderId).FirstOrDefault();
                if (teamLeader != null)
                {
                    if (teamLeader.Role.ID == 4)
                    {
                        teamLeader.Role = this.context.Role.Where(x => x.ID == 3).FirstOrDefault();
                    }
                    dbTeam.TeamLeader = teamLeader;
                }
            }

            if (newTeam.TeamManagerId != null)
            {
                DataModels.User teamManager = this.context.User.Where(x => x.PublicIdentifier == newTeam.TeamManagerId).FirstOrDefault();
                if (teamManager != null)
                {
                    dbTeam.TeamManager = teamManager;
                }
            }

            dbTeam.TeamMembers = new List<DataModels.User>();

            foreach (RegisterMember member in newTeam.TeamMembers)
            {
                DataModels.User teamMember = this.context.User.Where(x => x.PublicIdentifier == member.Id).FirstOrDefault();
                dbTeam.TeamMembers.Add(teamMember);
            }

            this.context.Team.Add(dbTeam);
            this.context.SaveChanges();
            ViewModels.Team resultTeam = new ViewModels.Team();
            resultTeam.Name = dbTeam.Name;
            resultTeam.ID = dbTeam.PublicIdentifier;
            return resultTeam;
        }

        private bool ValidateTeam(RegisterTeam newTeam, string userName)
        {
            if (String.IsNullOrEmpty(newTeam.Name))
            {
                return false;
            }

            DataModels.User postingUser = this.context.User.Include(x => x.Site).Where(x => x.EmailAddress == userName).FirstOrDefault();
            DataModels.Team[] siteTeams = this.context.Team.Include(x => x.Site).Where(x => x.Site == postingUser.Site).ToArray();

            foreach (var team in siteTeams)
            {
                if (team.Name == newTeam.Name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}