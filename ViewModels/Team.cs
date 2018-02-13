using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class Team
    {
        public string Name { get; set; }
        public string[] TeamMembers {get; set;}
        public Guid PublicIdentifier {get; set;}
        
    }
}