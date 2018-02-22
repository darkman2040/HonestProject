using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class Site
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int HoursPerDay { get; set; }
        public bool IncludeWeekends { get; set; }
        public string UniqueSiteId {get; set;}
    }
}