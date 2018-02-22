using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class TeamMember
    {
        public string Name {get; set;}
        public string Role {get; set;}
        public Guid PublicIdentifier {get; set;}
    }
}