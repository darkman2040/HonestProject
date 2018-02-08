using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;

namespace HonestProject.Repositories
{

    public class UserRepository : BasicRepository, IUserRepository
    {
        HonestProjectContext context;

        public UserRepository(HonestProjectContext context)
        {
            this.context = context;
        }

        public ViewModels.User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public ViewModels.User Save(ViewModels.User user)
        {
            try
            {
                if (!ValidateUser(user))
                {
                    base.ValidationFailed();
                    return null;
                }

                base.ValidationPassed();
                DataModels.User dbUser = new DataModels.User();
                dbUser.CreatedDate = DateTime.Now;
                dbUser.EmailAddress = user.EmailAddress;
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.PasswordHash = user.Password;
                dbUser.PublicIdentifier = Guid.NewGuid();
                DataModels.Site site = this.context.Site.Where(x => x.PublicIdentifier == user.UserSite).FirstOrDefault();
                dbUser.Site = site;
                this.context.User.Add(dbUser);
                this.context.SaveChanges();
                ViewModels.User viewUser = new ViewModels.User();
                viewUser.FirstName = dbUser.FirstName;
                viewUser.LastName = dbUser.LastName;
                viewUser.EmailAddress= dbUser.EmailAddress;
                viewUser.UserSite = site.PublicIdentifier;
                viewUser.ID = dbUser.PublicIdentifier;
                return viewUser;

            }
            catch (Exception e)
            {
                base.SetError(e.Message);
                return null;
            }

        }

        private bool ValidateUser(ViewModels.User user)
        {
            if (String.IsNullOrEmpty(user.FirstName) || user.FirstName.Length > 50)
            {
                return false;
            }

            if (String.IsNullOrEmpty(user.LastName) || user.LastName.Length > 50)
            {
                return false;
            }

            if (String.IsNullOrEmpty(user.EmailAddress) || user.EmailAddress.Length > 50)
            {
                return false;
            }

            if (user.UserSite == Guid.Empty)
            {
                return false;
            }

            DataModels.Site site = this.context.Site.Where(x => x.PublicIdentifier == user.UserSite).FirstOrDefault();
            if (site == null)
            {
                return false;
            }

            return true;

        }
    }
}