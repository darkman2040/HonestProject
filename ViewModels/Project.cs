using System;

namespace HonestProject.ViewModels
{
    public class Project{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime StartDate {get; set;}
        public int PercentageEstimate {get; set;}
        public string Color {get; set;}
    }
}