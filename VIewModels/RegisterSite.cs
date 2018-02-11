using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class RegisterSite
    {
        public string Name { get; set; }
        public int HoursPerDay { get; set; }
        public bool IncludeWeekends { get; set; }
        public string UniqueSiteId {get; set;}
    }
}