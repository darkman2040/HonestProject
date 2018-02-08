using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;


namespace HonestProject.Repositories
{
    public class SiteRepository : BasicRepository, ISiteRepository
    {
        HonestProjectContext context;

        public SiteRepository(HonestProjectContext context)
        {
            this.context = context;
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

        public ViewModels.Site Save(ViewModels.Site site)
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
                site.ID = dbSite.PublicIdentifier;
                return site;
                
            }
            catch(Exception e)
            {
                base.SetError(e.Message);
                return null;
            }
        }

        private bool ValidateSite(ViewModels.Site site)
        {
            if(site.Name.Length > 50)
            {
                return false;
            }

            if(site.HoursPerDay <= 0)
            {
                return false;
            }

            return true;
        }
    }
}