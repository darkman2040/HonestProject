using System.Collections.Generic;
using System.Linq;
using HonestProject.DataModels;
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

        public ViewModels.Project ConvertToViewProject(DataModels.Project project)
        {
            DataModels.Project dbProject = this.context.Project
            .Include(x => x.OwningTeam)
            .Include(x => x.WorkTypeItems)
            .ThenInclude(y => y.WorkType)
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
                viewWorkType.Name = workType.WorkType.Name;
                viewWorkType.ManHours = workType.ManHours;

                List<ViewModels.TimePercentageUserProjectWorkType> viewElements = new List<ViewModels.TimePercentageUserProjectWorkType>();

                List<DataModels.TimePercentageUserProjectWorkType> timeElements = this.context.TimePercentageUserProjectWorkType
                .Include(x => x.ProjectWorkType)
                .Include(x => x.User)
                .Where(x => x.ProjectWorkType.ID == workType.ID).ToList();
                
                foreach(var timeUnit in timeElements)
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