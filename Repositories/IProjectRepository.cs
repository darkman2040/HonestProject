namespace HonestProject.Repositories {
    public interface IProjectRepository : IBasicRepository
    {
         ViewModels.Project[] GetProjectsForTeam(string userId);
         ViewModels.ProjectTemplateTopLevel[] GetProjectTemplates(string userId);
    }
}