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
        IProjectTemplateConverter projectTemplateConverter;
        public ProjectRepository(HonestProjectContext context, IProjectConverter converter, IProjectTemplateConverter projectTemplateConverter)
        {
            this.context = context;
            this.converter = converter;
            this.projectTemplateConverter = projectTemplateConverter;
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

        public ViewModels.ProjectTemplateTopLevel[] GetProjectTemplates(string userId)
        {
            DataModels.User user = this.context.User.Include(x => x.Role)
            .Include(x => x.Team)
            .Where(x => x.EmailAddress == userId).FirstOrDefault();
            List<DataModels.ProjectTemplate> list = this.context.ProjectTemplate.ToList();
            List<ViewModels.ProjectTemplateTopLevel> viewList = new List<ProjectTemplateTopLevel>();
            foreach(var template in list)
            {
                viewList.Add(projectTemplateConverter.ConvertToTopLevelViewModel(template));
            }

            return viewList.ToArray();
        }

        public ViewModels.ProjectTemplateWorkType[] GetProjectTemplateWorkType(Guid projectTemplateId, string userId)
        {
            DataModels.User user = this.context.User.Include(x => x.Role).Where(x => x.EmailAddress == userId).FirstOrDefault();

            DataModels.ProjectTemplate projectTemplate = this.context.ProjectTemplate
            .Include(x => x.WorkTypes)
            .ThenInclude(x => x.WorkType)
            .Where(x => x.PublicIdentifier == projectTemplateId).FirstOrDefault();

            if(projectTemplate == null)
            {   
                this.ValidationFailed();
                return null;
            }

            this.ValidationPassed();

            List<ViewModels.ProjectTemplateWorkType> list = new List<ViewModels.ProjectTemplateWorkType>();
            foreach(var workType in projectTemplate.WorkTypes)
            {
                ViewModels.ProjectTemplateWorkType projectType = new ViewModels.ProjectTemplateWorkType();
                projectType.Name = workType.WorkType.Name;
                list.Add(projectType);
            } 

            return list.ToArray();
        }

        public ViewModels.Project RegisterNewProject(RegisterProject newProject, string userId)
        {
            if(!ValidateNewProject(newProject))
            {
                this.ValidationFailed();
                return null;
            }

            DataModels.Team team = GetTeamToAssignToProject(newProject, userId);
            if(team == null)
            {
                this.ValidationFailed();
                return null;
            }

            DataModels.Project dbProject = converter.ConvertNewProjectToDbProject(newProject, team);

            try
            {
            this.context.Project.Add(dbProject);
            this.context.SaveChanges();
            }
            catch(Exception e)
            {
                this.SetError(e.Message);
                return null;
            }

            ViewModels.Project viewProject = this.converter.ConvertToViewProject(dbProject);
            return viewProject;

            
        }

        private DataModels.Team GetTeamToAssignToProject(RegisterProject newProject, string userId)
        {
            DataModels.User user = this.context.User
            .Include(x => x.Role)
            .Where(x => x.EmailAddress == userId).FirstOrDefault();
            if(user.Role.ID == Const.RoleConst.TEAM_LEADER_ID)
            {
                DataModels.Team team = this.context.Team.Where(x => x.TeamLeader == user).FirstOrDefault();
                return team;
            }

            return null;
        }

        private bool ValidateNewProject(RegisterProject newProject)
        {
            if(String.IsNullOrEmpty(newProject.Name))
            {
                return false;
            }

            if(String.IsNullOrEmpty(newProject.Description))
            {
                return false;
            }

            if(String.IsNullOrEmpty(newProject.Color))
            {
                return false;
            }

            if(newProject.PercentageEstimate <= 0)
            {
                return false;
            }
            
            if(newProject.StartDate == null)
            {
                return false;
            }

            if(newProject.WorkTypeItems == null || newProject.WorkTypeItems.Count() <= 0)
            {
                return false;
            }

            return ValidateWorkTypes(newProject.WorkTypeItems);
        }

        private bool ValidateWorkTypes(RegisterProjectWorkType[] workTypeItems)
        {
            foreach(RegisterProjectWorkType workType in workTypeItems)
            {
                if(String.IsNullOrEmpty(workType.Name))
                {
                    return false;
                }

                if(workType.ManHours <= 0)
                {
                    return false;
                }

                bool validatePct = ValidatePctItems(workType.TimePctWorkItems);
                if(!validatePct)
                {
                    return false;
                }   
            }

            return true;
        }

        private bool ValidatePctItems(RegisterProjectTimePct[] timePctWorkItems)
        {
            foreach(RegisterProjectTimePct pct in timePctWorkItems)
            {
                if(pct.WorkPercentage <= 0)
                {
                    return false;
                }

                if(pct.UserId == null)
                {
                    return false;
                }

                DataModels.User user = this.context.User.Where(x => x.PublicIdentifier == pct.UserId).FirstOrDefault();
                if(user == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}