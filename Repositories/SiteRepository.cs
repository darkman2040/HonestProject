using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Repositories
{
    public class SiteRepository : BasicRepository, ISiteRepository
    {
        HonestProjectContext context;
        IConfiguration configuration;

        public SiteRepository(HonestProjectContext context, IConfiguration config)
        {
            this.context = context;
            this.configuration = config;
        }
        public ViewModels.Site GetSite(Guid id)
        {
            try
            {
                DataModels.Site site = this.context.Site.Where(f => f.PublicIdentifier == id).FirstOrDefault();
                if (site == null)
                {
                    base.ValidationFailed();
                    return null;
                }

                base.ValidationPassed();
                ViewModels.Site viewSite = new ViewModels.Site();
                viewSite.HoursPerDay = site.HoursPerDay;
                viewSite.IncludeWeekends = site.IncludeWeekends;
                viewSite.Name = site.Name;
                viewSite.ID = site.PublicIdentifier;
                return viewSite;
            }
            catch (Exception e)
            {
                base.SetError(e.Message);
                return null;
            }

            
        }

        public ViewModels.Site Save(ViewModels.RegisterSite site)
        {
            try
            {
                if(!ValidateSite(site))
                {
                    base.ValidationFailed();
                    return null;
                }

                base.ValidationPassed();

                DataModels.Site dbSite = new DataModels.Site();
                dbSite.HoursPerDay = site.HoursPerDay;
                dbSite.IncludeWeekends = site.IncludeWeekends;
                dbSite.Name = site.Name; 
                dbSite.PublicIdentifier = Guid.NewGuid();
                this.context.Site.Add(dbSite);
                this.context.SaveChanges();
                ViewModels.Site viewSite = new ViewModels.Site();
                viewSite.UniqueSiteId = dbSite.UniqueSiteId;
                viewSite.HoursPerDay = dbSite.HoursPerDay;
                viewSite.ID = dbSite.PublicIdentifier;
                viewSite.IncludeWeekends = dbSite.IncludeWeekends;
                viewSite.Name = dbSite.Name;
                return viewSite;
                
            }
            catch(Exception e)
            {
                base.SetError(e.Message);
                return null;
            }
        }

        private bool ValidateSite(ViewModels.RegisterSite site)
        {
            //Can't save more than one site in single site mode
            if(this.configuration["SingleSiteMode"] == "true")
            {
                if(this.context.Site.Count() > 0)
                {
                    return false;
                }
            }

            if(String.IsNullOrEmpty(site.Name) || site.Name.Length > 50)
            {
                return false;
            }

            if(site.HoursPerDay <= 0)
            {
                return false;
            }

            if(String.IsNullOrEmpty(site.UniqueSiteId) || site.UniqueSiteId.Length > 100)
            {
                return false;
            }

            return true;
        }
    }
}