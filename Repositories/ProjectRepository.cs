using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.Const;
using HonestProject.Converters;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Repositories
{
    public class ProjectRepository : BasicRepository, IProjectRepository
    {
        private HonestProjectContext context;
        private IProjectConverter converter;
        public ProjectRepository(HonestProjectContext context, IProjectConverter converter)
        {
            this.context = context;
            this.converter = converter;
        }
        public ViewModels.Project[] GetProjectsForTeam(string userId)
        {
            DataModels.User user = this.context.User.Include(x => x.Role).Where(x => x.EmailAddress == userId).FirstOrDefault();
            List<DataModels.Project> list = new List<DataModels.Project>();
            if(user.Role.ID == RoleConst.TEAM_LEADER_ID)
            {
                DataModels.Team team = this.context.Team.Where(x => x.TeamLeader == user).FirstOrDefault();
                list = this.context.Project.Where(x => x.OwningTeam == team).ToList();
            }

            //TODO: Get teams for Team Manager level and Site Admin

            List<ViewModels.Project> viewList = new List<ViewModels.Project>();

            foreach(var project in list)
            {
                viewList.Add(converter.ConvertToViewProject(project));
            }

            return viewList.ToArray();
        }
    }
}