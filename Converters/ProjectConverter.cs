using System.Collections.Generic;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HonestProject.Converters
{
    public class ProjectConverter : IProjectConverter
    {
        HonestProjectContext context;
        public ProjectConverter(HonestProjectContext context)
        {
            this.context = context;
        }

        public DataModels.Project ConvertNewProjectToDbProject(ViewModels.RegisterProject newProject, DataModels.Team owner)
        {
            DataModels.Project dbProject = new DataModels.Project();
            dbProject.PublicIdentifier = new System.Guid();
            dbProject.Name = newProject.Name;
            dbProject.Description = newProject.Description;
            dbProject.Color = newProject.Color;
            dbProject.PercentageEstimate = newProject.PercentageEstimate;
            dbProject.StartDate = newProject.StartDate;
            dbProject.OwningTeam = owner;
            dbProject.WorkTypeItems = new List<DataModels.ProjectWorkType>();
            foreach(ViewModels.RegisterProjectWorkType workType in newProject.WorkTypeItems)
            {
                DataModels.ProjectWorkType projectType = new DataModels.ProjectWorkType();
                projectType.Name = workType.Name;
                projectType.PublicIdentifier = new System.Guid();
                projectType.ManHours = workType.ManHours;
                projectType.TimePctWorkItems = new List<DataModels.TimePercentageUserProjectWorkType>();
                foreach(ViewModels.RegisterProjectTimePct timePct in workType.TimePctWorkItems)
                {
                    DataModels.TimePercentageUserProjectWorkType dbTimePct = new DataModels.TimePercentageUserProjectWorkType();
                    dbTimePct.PublicIdentifier = new System.Guid();
                    dbTimePct.WorkPercentage = timePct.WorkPercentage;
                    DataModels.User user = this.context.User.Where(x => x.PublicIdentifier == timePct.UserId).FirstOrDefault();
                    projectType.TimePctWorkItems.Add(dbTimePct);
                }
                dbProject.WorkTypeItems.Add(projectType);
            }

            return dbProject;
        }

        public ViewModels.Project ConvertToViewProject(DataModels.Project project)
        {
            DataModels.Project dbProject = this.context.Project
            .Include(x => x.OwningTeam)
            .Include(x => x.WorkTypeItems)
            .ThenInclude(x => x.TimePctWorkItems)
            .ThenInclude(x => x.User)
            .FirstOrDefault();

            ViewModels.Project viewProject = new ViewModels.Project();
            viewProject.Id = project.PublicIdentifier;
            viewProject.Color = project.Color;
            viewProject.Name = project.Name;
            viewProject.Description = project.Description;
            viewProject.PercentageEstimate = project.PercentageEstimate;
            viewProject.StartDate = project.StartDate;
            viewProject.TeamId = project.OwningTeam.PublicIdentifier;
            List<ViewModels.ProjectWorkType> workTypes = new List<ViewModels.ProjectWorkType>();
            foreach(var workType in dbProject.WorkTypeItems)
            {
                ViewModels.ProjectWorkType viewWorkType = new ViewModels.ProjectWorkType();
                viewWorkType.Id = workType.PublicIdentifier;
                viewWorkType.Name = workType.Name;
                viewWorkType.ManHours = workType.ManHours;

                List<ViewModels.TimePercentageUserProjectWorkType> viewElements = new List<ViewModels.TimePercentageUserProjectWorkType>();
                
                foreach(var timeUnit in workType.TimePctWorkItems)
                {
                    ViewModels.TimePercentageUserProjectWorkType viewElement = new ViewModels.TimePercentageUserProjectWorkType();
                    viewElement.Id = timeUnit.PublicIdentifier;
                    viewElement.FirstName = timeUnit.User.FirstName;
                    viewElement.LastName = timeUnit.User.LastName;
                    viewElement.WorkPercentage = timeUnit.WorkPercentage;

                    viewElements.Add(viewElement);
                }

                viewWorkType.UserWorkList = viewElements.ToArray();
                workTypes.Add(viewWorkType);
            }

            viewProject.WorkTypes = workTypes.ToArray();
            return viewProject;
        }
    }
}