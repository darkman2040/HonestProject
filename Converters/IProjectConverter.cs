namespace HonestProject.Converters
{
    public interface IProjectConverter
    {
        ViewModels.Project ConvertToViewProject(DataModels.Project project);
        DataModels.Project ConvertNewProjectToDbProject(ViewModels.RegisterProject newProject, DataModels.Team owner);
    }
}