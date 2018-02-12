using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class User{
        public Guid ID{get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string EmailAddress {get; set;}
        public Guid UserSite {get; set;}
        public bool IsSiteAdmin{get; set;}
        public bool IsManager{get;set;}
        public bool IsTeamLeader{get; set;}
    }
}