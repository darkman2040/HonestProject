using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Repositories
{

    public class UserRepository : BasicRepository, IUserRepository
    {
        HonestProjectContext context;
        IConfiguration configuration;

        public UserRepository(HonestProjectContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public ViewModels.User GetUser(Guid id)
        {
            try
            {
                DataModels.User dbUser = this.context.User.Include(x => x.Site).Where(x => x.PublicIdentifier == id).FirstOrDefault();
                if (dbUser == null)
                {
                    this.ValidationFailed();
                }

                this.ValidationPassed();
                ViewModels.User user = new ViewModels.User();
                user.ID = dbUser.PublicIdentifier;
                user.EmailAddress = dbUser.EmailAddress;
                user.FirstName = dbUser.FirstName;
                user.LastName = dbUser.LastName;
                user.UserSite = dbUser.Site.PublicIdentifier;
                return user;
            }
            catch (Exception e)
            {
                this.SetError(e.Message);
                return null;
            }
        }

        public ViewModels.User Save(ViewModels.RegisterUser user)
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
                DataModels.Site site = null;
                if (this.configuration["SingleSiteMode"].ToLower() == "true")
                {
                    site = this.context.Site.FirstOrDefault();
                    dbUser.Site = site;
                }
                this.context.User.Add(dbUser);
                this.context.SaveChanges();
                ViewModels.User viewUser = new ViewModels.User();
                viewUser.FirstName = dbUser.FirstName;
                viewUser.LastName = dbUser.LastName;
                viewUser.EmailAddress = dbUser.EmailAddress;
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

        private bool ValidateUser(ViewModels.RegisterUser user)
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

            if (this.configuration["SingleSiteMode"].ToLower() == "true")
            {
                DataModels.Site site = this.context.Site.FirstOrDefault();
                if (site == null)
                {
                    return false;
                }
            }
            else
            {
                //Halt registration for now
                return false;
            }

            return true;

        }
    }
}