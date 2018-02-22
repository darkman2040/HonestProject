namespace HonestProject.Converters
{
    public interface IProjectConverter
    {
        ViewModels.Project ConvertToViewProject(DataModels.Project project);
    }
}