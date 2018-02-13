using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class RegisterTeam
    {
        public string Name { get; set; }
        public Guid[] TeamMembers {get; set;}
        public Guid TeamLeader {get; set;}
        public Guid TeamManager {get; set;}
    }
}