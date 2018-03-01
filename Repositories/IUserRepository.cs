using System;
using HonestProject.ViewModels;

namespace HonestProject.Repositories
{
    public interface IUserRepository : IBasicRepository
    {
        User GetUser(Guid id);
        User[] GetUnassignedUsers(string userName);
        User Save(RegisterUser user);
        User[] GetTeamMembers(Guid teamId, string userName);
    }
}