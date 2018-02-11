using System;

namespace HonestProject.Repositories
{
    public interface ISiteRepository : IBasicRepository
    {
        ViewModels.Site Save(HonestProject.ViewModels.RegisterSite site);
        ViewModels.Site GetSite(Guid id);
        bool CanRegisterSite();
    }
}