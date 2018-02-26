namespace HonestProject.Converters
{
    public interface IProjectTemplateConverter
    {
        ViewModels.ProjectTemplateTopLevel ConvertToTopLevelViewModel(DataModels.ProjectTemplate project);
    }
}