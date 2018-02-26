using System;

namespace HonestProject.ViewModels
{
    public class ProjectWorkType
    {
        public Guid Id { get; set; }
        public string Name {get; set;}
        public int ManHours {get; set;}
        public TimePercentageUserProjectWorkType[] UserWorkList {get; set;}
    }
}