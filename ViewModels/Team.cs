using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class Team
    {
        public string Name { get; set; }
        public string Description {get; set;}
        public Guid TeamLeaderId {get; set;}
        public Guid TeamManagerId {get; set;}
        public TeamMember[] TeamMembers {get; set;}
        public Guid ID {get; set;}
        
    }
}