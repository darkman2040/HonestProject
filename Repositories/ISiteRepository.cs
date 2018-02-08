using System;

namespace HonestProject.Repositories
{
    public interface ISiteRepository : IBasicRepository
    {
        ViewModels.Site Save(HonestProject.ViewModels.Site site);
        ViewModels.Site GetSite(Guid id);
    }
}