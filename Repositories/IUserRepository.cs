using System;
using HonestProject.ViewModels;

namespace HonestProject.Repositories
{
    public interface IUserRepository : IBasicRepository
    {
        User GetUser(Guid id);
        User Save(RegisterUser user);
    }
}