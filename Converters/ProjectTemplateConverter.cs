using HonestProject.DataModels;

namespace HonestProject.Converters
{
    public class ProjectTemplateConverter : IProjectTemplateConverter
    {
        HonestProjectContext context;
        public ProjectTemplateConverter(HonestProjectContext context)
        {
            this.context = context;
        }
        public ViewModels.ProjectTemplateTopLevel ConvertToTopLevelViewModel(DataModels.ProjectTemplate project)
        {
            ViewModels.ProjectTemplateTopLevel viewProject = new ViewModels.ProjectTemplateTopLevel();
            viewProject.Name = project.Name;
            viewProject.Id = project.PublicIdentifier;
            return viewProject;
        }
    }
}