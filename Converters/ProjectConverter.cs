namespace HonestProject.Converters
{
    public class ProjectConverter : IProjectConverter
    {
        public ViewModels.Project ConvertToViewProject(DataModels.Project project)
        {
            ViewModels.Project viewProject = new ViewModels.Project();
            viewProject.Id = project.PublicIdentifier;
            viewProject.Color = project.Color;
            viewProject.Name = project.Name;
            viewProject.PercentageEstimate = project.PercentageEstimate;
            viewProject.StartDate = project.StartDate;
            return viewProject;
        }
    }
}