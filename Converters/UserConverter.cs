using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HonestProject.Converters
{
    public class UserConverter : IUserConverter
    {
        HonestProjectContext context;
        public UserConverter(HonestProjectContext context)
        {
            this.context = context;
        }
        public ViewModels.User ConvertFromDbUser(DataModels.User user)
        {
            DataModels.User dbUser = this.context.User
            .Include(x => x.Site)
            .Include(x => x.Role)
            .Where(x => x.PublicIdentifier == user.PublicIdentifier).FirstOrDefault();
            ViewModels.User viewUser = new ViewModels.User();
            viewUser.FirstName = user.FirstName;
            viewUser.LastName = user.LastName;
            viewUser.EmailAddress = user.EmailAddress;
            viewUser.userId = user.PublicIdentifier;
            viewUser.UserSite = dbUser.Site.PublicIdentifier;
            SetSecurityParams(viewUser, dbUser);
            return viewUser;
        }

        private void SetSecurityParams(ViewModels.User user, DataModels.User dbUser)
        {
            if (dbUser.Role.Name == "Site Administrator")
            {
                user.IsSiteAdmin = true;
                user.IsManager = true;
                user.IsTeamLeader = true;
                return;
            }

            if (dbUser.Role.Name == "Manager")
            {
                user.IsSiteAdmin = false;
                user.IsManager = true;
                user.IsTeamLeader = true;
                return;
            }

            if (dbUser.Role.Name == "Team Leader")
            {
                user.IsSiteAdmin = false;
                user.IsManager = false;
                user.IsTeamLeader = true;
                return;
            }
        }
    }
}